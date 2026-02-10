using Xunit;
using System;
using TourManagement.Application.DTOs;

namespace TourManagement.Application.DTOs.Tests;

public class UserDtoTests
{
    [Fact]
    public void UserDto_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new UserDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Email);
        Assert.Equal(string.Empty, dto.FirstName);
        Assert.Equal(string.Empty, dto.LastName);
        Assert.Null(dto.PhoneNumber);
        Assert.Equal(default(DateTime), dto.CreatedDate);
        Assert.False(dto.IsActive);
    }

    [Fact]
    public void UserDto_ShouldSetAndGetAllProperties()
    {
        // Arrange
        var dto = new UserDto();
        var expectedId = 100;
        var expectedEmail = "john.doe@example.com";
        var expectedFirstName = "John";
        var expectedLastName = "Doe";
        var expectedPhone = "+1234567890";
        var expectedCreatedDate = DateTime.UtcNow;
        var expectedIsActive = true;

        // Act
        dto.Id = expectedId;
        dto.Email = expectedEmail;
        dto.FirstName = expectedFirstName;
        dto.LastName = expectedLastName;
        dto.PhoneNumber = expectedPhone;
        dto.CreatedDate = expectedCreatedDate;
        dto.IsActive = expectedIsActive;

        // Assert
        Assert.Equal(expectedId, dto.Id);
        Assert.Equal(expectedEmail, dto.Email);
        Assert.Equal(expectedFirstName, dto.FirstName);
        Assert.Equal(expectedLastName, dto.LastName);
        Assert.Equal(expectedPhone, dto.PhoneNumber);
        Assert.Equal(expectedCreatedDate, dto.CreatedDate);
        Assert.Equal(expectedIsActive, dto.IsActive);
    }

    [Fact]
    public void UserDto_PhoneNumber_ShouldAcceptNull()
    {
        // Arrange
        var dto = new UserDto();

        // Act
        dto.PhoneNumber = null;

        // Assert
        Assert.Null(dto.PhoneNumber);
    }
}

public class UserCreateDtoTests
{
    [Fact]
    public void UserCreateDto_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new UserCreateDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Email);
        Assert.Equal(string.Empty, dto.Password);
        Assert.Equal(string.Empty, dto.FirstName);
        Assert.Equal(string.Empty, dto.LastName);
        Assert.Null(dto.PhoneNumber);
        Assert.Equal("System", dto.CreatedBy);
    }

    [Fact]
    public void UserCreateDto_ShouldSetAndGetAllProperties()
    {
        // Arrange
        var dto = new UserCreateDto();
        var expectedEmail = "newuser@example.com";
        var expectedPassword = "SecurePassword123!";
        var expectedFirstName = "Jane";
        var expectedLastName = "Smith";
        var expectedPhone = "+9876543210";
        var expectedCreatedBy = "admin@example.com";

        // Act
        dto.Email = expectedEmail;
        dto.Password = expectedPassword;
        dto.FirstName = expectedFirstName;
        dto.LastName = expectedLastName;
        dto.PhoneNumber = expectedPhone;
        dto.CreatedBy = expectedCreatedBy;

        // Assert
        Assert.Equal(expectedEmail, dto.Email);
        Assert.Equal(expectedPassword, dto.Password);
        Assert.Equal(expectedFirstName, dto.FirstName);
        Assert.Equal(expectedLastName, dto.LastName);
        Assert.Equal(expectedPhone, dto.PhoneNumber);
        Assert.Equal(expectedCreatedBy, dto.CreatedBy);
    }

    [Fact]
    public void UserCreateDto_CreatedBy_ShouldDefaultToSystem()
    {
        // Arrange & Act
        var dto = new UserCreateDto();

        // Assert
        Assert.Equal("System", dto.CreatedBy);
    }

    [Fact]
    public void UserCreateDto_PhoneNumber_ShouldAcceptNull()
    {
        // Arrange
        var dto = new UserCreateDto();

        // Act
        dto.PhoneNumber = null;

        // Assert
        Assert.Null(dto.PhoneNumber);
    }

    [Fact]
    public void UserCreateDto_ShouldHandleEmptyPassword()
    {
        // Arrange
        var dto = new UserCreateDto();

        // Act
        dto.Password = string.Empty;

        // Assert
        Assert.Equal(string.Empty, dto.Password);
    }
}

public class UserUpdateDtoTests
{
    [Fact]
    public void UserUpdateDto_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new UserUpdateDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.FirstName);
        Assert.Equal(string.Empty, dto.LastName);
        Assert.Null(dto.PhoneNumber);
        Assert.Equal("System", dto.ModifiedBy);
    }

    [Fact]
    public void UserUpdateDto_ShouldSetAndGetAllProperties()
    {
        // Arrange
        var dto = new UserUpdateDto();
        var expectedFirstName = "Updated";
        var expectedLastName = "User";
        var expectedPhone = "+1111111111";
        var expectedModifiedBy = "editor@example.com";

        // Act
        dto.FirstName = expectedFirstName;
        dto.LastName = expectedLastName;
        dto.PhoneNumber = expectedPhone;
        dto.ModifiedBy = expectedModifiedBy;

        // Assert
        Assert.Equal(expectedFirstName, dto.FirstName);
        Assert.Equal(expectedLastName, dto.LastName);
        Assert.Equal(expectedPhone, dto.PhoneNumber);
        Assert.Equal(expectedModifiedBy, dto.ModifiedBy);
    }

    [Fact]
    public void UserUpdateDto_ModifiedBy_ShouldDefaultToSystem()
    {
        // Arrange & Act
        var dto = new UserUpdateDto();

        // Assert
        Assert.Equal("System", dto.ModifiedBy);
    }

    [Fact]
    public void UserUpdateDto_PhoneNumber_ShouldAcceptNull()
    {
        // Arrange
        var dto = new UserUpdateDto();

        // Act
        dto.PhoneNumber = null;

        // Assert
        Assert.Null(dto.PhoneNumber);
    }
}

public class UserLoginDtoTests
{
    [Fact]
    public void UserLoginDto_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new UserLoginDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.Email);
        Assert.Equal(string.Empty, dto.Password);
    }

    [Fact]
    public void UserLoginDto_ShouldSetAndGetEmail()
    {
        // Arrange
        var dto = new UserLoginDto();
        var expectedEmail = "login@example.com";

        // Act
        dto.Email = expectedEmail;

        // Assert
        Assert.Equal(expectedEmail, dto.Email);
    }

    [Fact]
    public void UserLoginDto_ShouldSetAndGetPassword()
    {
        // Arrange
        var dto = new UserLoginDto();
        var expectedPassword = "MyPassword123";

        // Act
        dto.Password = expectedPassword;

        // Assert
        Assert.Equal(expectedPassword, dto.Password);
    }

    [Fact]
    public void UserLoginDto_ShouldHandleEmptyCredentials()
    {
        // Arrange
        var dto = new UserLoginDto();

        // Act
        dto.Email = string.Empty;
        dto.Password = string.Empty;

        // Assert
        Assert.Equal(string.Empty, dto.Email);
        Assert.Equal(string.Empty, dto.Password);
    }
}

public class UserAuthDtoTests
{
    [Fact]
    public void UserAuthDto_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new UserAuthDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.Email);
        Assert.Equal(string.Empty, dto.FirstName);
        Assert.Equal(string.Empty, dto.LastName);
    }

    [Fact]
    public void UserAuthDto_ShouldSetAndGetAllProperties()
    {
        // Arrange
        var dto = new UserAuthDto();
        var expectedId = 777;
        var expectedEmail = "auth@example.com";
        var expectedFirstName = "Auth";
        var expectedLastName = "User";

        // Act
        dto.Id = expectedId;
        dto.Email = expectedEmail;
        dto.FirstName = expectedFirstName;
        dto.LastName = expectedLastName;

        // Assert
        Assert.Equal(expectedId, dto.Id);
        Assert.Equal(expectedEmail, dto.Email);
        Assert.Equal(expectedFirstName, dto.FirstName);
        Assert.Equal(expectedLastName, dto.LastName);
    }

    [Fact]
    public void UserAuthDto_ShouldHandleZeroId()
    {
        // Arrange
        var dto = new UserAuthDto();

        // Act
        dto.Id = 0;

        // Assert
        Assert.Equal(0, dto.Id);
    }

    [Fact]
    public void UserAuthDto_ShouldHandleNegativeId()
    {
        // Arrange
        var dto = new UserAuthDto();

        // Act
        dto.Id = -1;

        // Assert
        Assert.Equal(-1, dto.Id);
    }
}
