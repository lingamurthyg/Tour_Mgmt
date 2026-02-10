using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourManagement.Application.Services;
using TourManagement.Application.DTOs;
using TourManagement.Domain.Entities;
using TourManagement.Domain.Exceptions;
using TourManagement.Domain.Interfaces.Repositories;

namespace TourManagement.Application.Services.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<UserService>> _mockLogger;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<UserService>>();
        _userService = new UserService(_mockUserRepository.Object, _mockMapper.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_ShouldInitializeService()
    {
        // Arrange & Act
        var service = new UserService(_mockUserRepository.Object, _mockMapper.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Email = "user1@example.com" },
            new User { Id = 2, Email = "user2@example.com" }
        };
        var userDtos = new List<UserDto>
        {
            new UserDto { Id = 1, Email = "user1@example.com" },
            new UserDto { Id = 2, Email = "user2@example.com" }
        };

        _mockUserRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);
        _mockMapper.Setup(m => m.Map<IEnumerable<UserDto>>(users))
            .Returns(userDtos);

        // Act
        var result = await _userService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        _mockUserRepository.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyListWhenNoUsers()
    {
        // Arrange
        var users = new List<User>();
        var userDtos = new List<UserDto>();

        _mockUserRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);
        _mockMapper.Setup(m => m.Map<IEnumerable<UserDto>>(users))
            .Returns(userDtos);

        // Act
        var result = await _userService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        _mockUserRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.GetAllAsync());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUserWhenExists()
    {
        // Arrange
        var userId = 123;
        var user = new User { Id = userId, Email = "test@example.com" };
        var userDto = new UserDto { Id = userId, Email = "test@example.com" };

        _mockUserRepository.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        _mockMapper.Setup(m => m.Map<UserDto>(user))
            .Returns(userDto);

        // Act
        var result = await _userService.GetByIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        _mockUserRepository.Verify(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNullWhenUserNotFound()
    {
        // Arrange
        var userId = 999;
        _mockUserRepository.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.GetByIdAsync(userId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var userId = 1;
        _mockUserRepository.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.GetByIdAsync(userId));
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateAndReturnUserWhenEmailNotExists()
    {
        // Arrange
        var createDto = new UserCreateDto { Email = "newuser@example.com", Password = "password123" };
        var user = new User { Email = "newuser@example.com" };
        var createdUser = new User { Id = 1, Email = "newuser@example.com" };
        var userDto = new UserDto { Id = 1, Email = "newuser@example.com" };

        _mockUserRepository.Setup(r => r.EmailExistsAsync(createDto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _mockMapper.Setup(m => m.Map<User>(createDto))
            .Returns(user);
        _mockUserRepository.Setup(r => r.AddAsync(user, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdUser);
        _mockMapper.Setup(m => m.Map<UserDto>(createdUser))
            .Returns(userDto);

        // Act
        var result = await _userService.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        _mockUserRepository.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowBusinessRuleValidationExceptionWhenEmailExists()
    {
        // Arrange
        var createDto = new UserCreateDto { Email = "existing@example.com", Password = "password123" };

        _mockUserRepository.Setup(r => r.EmailExistsAsync(createDto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<BusinessRuleValidationException>(() => _userService.CreateAsync(createDto));
        _mockUserRepository.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var createDto = new UserCreateDto { Email = "newuser@example.com", Password = "password123" };
        var user = new User { Email = "newuser@example.com" };

        _mockUserRepository.Setup(r => r.EmailExistsAsync(createDto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _mockMapper.Setup(m => m.Map<User>(createDto))
            .Returns(user);
        _mockUserRepository.Setup(r => r.AddAsync(user, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.CreateAsync(createDto));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateUserWhenExists()
    {
        // Arrange
        var userId = 10;
        var updateDto = new UserUpdateDto { FirstName = "Updated" };
        var existingUser = new User { Id = userId, FirstName = "Old" };

        _mockUserRepository.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);
        _mockMapper.Setup(m => m.Map(updateDto, existingUser))
            .Returns(existingUser);
        _mockUserRepository.Setup(r => r.UpdateAsync(existingUser, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _userService.UpdateAsync(userId, updateDto);

        // Assert
        _mockUserRepository.Verify(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
        _mockUserRepository.Verify(r => r.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowEntityNotFoundExceptionWhenUserNotExists()
    {
        // Arrange
        var userId = 999;
        var updateDto = new UserUpdateDto { FirstName = "Updated" };

        _mockUserRepository.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _userService.UpdateAsync(userId, updateDto));
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var userId = 10;
        var updateDto = new UserUpdateDto { FirstName = "Updated" };
        var existingUser = new User { Id = userId, FirstName = "Old" };

        _mockUserRepository.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);
        _mockMapper.Setup(m => m.Map(updateDto, existingUser))
            .Returns(existingUser);
        _mockUserRepository.Setup(r => r.UpdateAsync(existingUser, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.UpdateAsync(userId, updateDto));
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteUserWhenExists()
    {
        // Arrange
        var userId = 5;
        _mockUserRepository.Setup(r => r.ExistsAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _mockUserRepository.Setup(r => r.DeleteAsync(userId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _userService.DeleteAsync(userId);

        // Assert
        _mockUserRepository.Verify(r => r.ExistsAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
        _mockUserRepository.Verify(r => r.DeleteAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowEntityNotFoundExceptionWhenUserNotExists()
    {
        // Arrange
        var userId = 999;
        _mockUserRepository.Setup(r => r.ExistsAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _userService.DeleteAsync(userId));
        _mockUserRepository.Verify(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var userId = 5;
        _mockUserRepository.Setup(r => r.ExistsAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _mockUserRepository.Setup(r => r.DeleteAsync(userId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.DeleteAsync(userId));
    }

    [Fact]
    public async Task AuthenticateAsync_ShouldReturnUserAuthDtoWhenCredentialsValid()
    {
        // Arrange
        var email = "user@example.com";
        var password = "password123";
        var passwordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        var user = new User { Id = 1, Email = email, PasswordHash = passwordHash };
        var userAuthDto = new UserAuthDto { Id = 1, Email = email };

        _mockUserRepository.Setup(r => r.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        _mockMapper.Setup(m => m.Map<UserAuthDto>(user))
            .Returns(userAuthDto);

        // Act
        var result = await _userService.AuthenticateAsync(email, password);

        // Assert
        Assert.NotNull(result);
        _mockUserRepository.Verify(r => r.GetByEmailAsync(email, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AuthenticateAsync_ShouldReturnNullWhenUserNotFound()
    {
        // Arrange
        var email = "nonexistent@example.com";
        var password = "password123";

        _mockUserRepository.Setup(r => r.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.AuthenticateAsync(email, password);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AuthenticateAsync_ShouldReturnNullWhenPasswordInvalid()
    {
        // Arrange
        var email = "user@example.com";
        var password = "wrongpassword";
        var correctPasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("correctpassword"));
        var user = new User { Id = 1, Email = email, PasswordHash = correctPasswordHash };

        _mockUserRepository.Setup(r => r.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.AuthenticateAsync(email, password);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AuthenticateAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var email = "user@example.com";
        var password = "password123";

        _mockUserRepository.Setup(r => r.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.AuthenticateAsync(email, password));
    }

    [Fact]
    public async Task EmailExistsAsync_ShouldReturnTrueWhenEmailExists()
    {
        // Arrange
        var email = "existing@example.com";
        _mockUserRepository.Setup(r => r.EmailExistsAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _userService.EmailExistsAsync(email);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task EmailExistsAsync_ShouldReturnFalseWhenEmailNotExists()
    {
        // Arrange
        var email = "nonexistent@example.com";
        _mockUserRepository.Setup(r => r.EmailExistsAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _userService.EmailExistsAsync(email);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task EmailExistsAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var email = "test@example.com";
        _mockUserRepository.Setup(r => r.EmailExistsAsync(email, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.EmailExistsAsync(email));
    }
}
