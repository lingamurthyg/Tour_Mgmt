using Xunit;
using AutoMapper;
using System;
using TourManagement.Application.Mappings;
using TourManagement.Application.DTOs;
using TourManagement.Domain.Entities;

namespace TourManagement.Application.Mappings.Tests;

public class MappingProfileTests
{
    private readonly IMapper _mapper;
    private readonly MapperConfiguration _configuration;

    public MappingProfileTests()
    {
        _configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = _configuration.CreateMapper();
    }

    [Fact]
    public void MappingProfile_ShouldHaveValidConfiguration()
    {
        // Act & Assert
        _configuration.AssertConfigurationIsValid();
    }

    [Fact]
    public void Tour_ToTourDto_ShouldMapCorrectly()
    {
        // Arrange
        var tour = new Tour
        {
            Id = 1,
            TourName = "Test Tour",
            Place = "Europe",
            Days = 7,
            Price = 1999.99m,
            Locations = "Paris, Rome",
            TourInfo = "Great tour",
            PicturePath = "/images/tour.jpg",
            CreatedDate = DateTime.UtcNow,
            IsActive = true
        };

        // Act
        var result = _mapper.Map<TourDto>(tour);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tour.Id, result.Id);
        Assert.Equal(tour.TourName, result.TourName);
        Assert.Equal(tour.Place, result.Place);
        Assert.Equal(tour.Days, result.Days);
        Assert.Equal(tour.Price, result.Price);
        Assert.Equal(tour.Locations, result.Locations);
        Assert.Equal(tour.TourInfo, result.TourInfo);
        Assert.Equal(tour.PicturePath, result.PicturePath);
        Assert.Equal(tour.IsActive, result.IsActive);
    }

    [Fact]
    public void TourCreateDto_ToTour_ShouldMapCorrectly()
    {
        // Arrange
        var createDto = new TourCreateDto
        {
            TourName = "New Tour",
            Place = "Asia",
            Days = 10,
            Price = 2500.00m,
            Locations = "Tokyo, Seoul",
            TourInfo = "Amazing tour",
            PicturePath = "/images/new.jpg",
            CreatedBy = "admin"
        };

        // Act
        var result = _mapper.Map<Tour>(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createDto.TourName, result.TourName);
        Assert.Equal(createDto.Place, result.Place);
        Assert.Equal(createDto.Days, result.Days);
        Assert.Equal(createDto.Price, result.Price);
        Assert.Equal(createDto.Locations, result.Locations);
        Assert.Equal(createDto.TourInfo, result.TourInfo);
        Assert.Equal(createDto.PicturePath, result.PicturePath);
        Assert.Equal(createDto.CreatedBy, result.CreatedBy);
        Assert.True(result.IsActive);
        Assert.NotEqual(default(DateTime), result.CreatedDate);
    }

    [Fact]
    public void TourUpdateDto_ToTour_ShouldMapCorrectly()
    {
        // Arrange
        var updateDto = new TourUpdateDto
        {
            TourName = "Updated Tour",
            Place = "America",
            Days = 14,
            Price = 3000.00m,
            Locations = "New York, LA",
            TourInfo = "Updated info",
            PicturePath = "/images/updated.jpg",
            ModifiedBy = "editor"
        };

        // Act
        var result = _mapper.Map<Tour>(updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updateDto.TourName, result.TourName);
        Assert.Equal(updateDto.Place, result.Place);
        Assert.Equal(updateDto.Days, result.Days);
        Assert.Equal(updateDto.Price, result.Price);
        Assert.Equal(updateDto.Locations, result.Locations);
        Assert.Equal(updateDto.TourInfo, result.TourInfo);
        Assert.Equal(updateDto.PicturePath, result.PicturePath);
        Assert.Equal(updateDto.ModifiedBy, result.ModifiedBy);
    }

    [Fact]
    public void User_ToUserDto_ShouldMapCorrectly()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Email = "user@example.com",
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "+1234567890",
            CreatedDate = DateTime.UtcNow,
            IsActive = true
        };

        // Act
        var result = _mapper.Map<UserDto>(user);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.FirstName, result.FirstName);
        Assert.Equal(user.LastName, result.LastName);
        Assert.Equal(user.PhoneNumber, result.PhoneNumber);
        Assert.Equal(user.IsActive, result.IsActive);
    }

    [Fact]
    public void User_ToUserAuthDto_ShouldMapCorrectly()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Email = "user@example.com",
            FirstName = "John",
            LastName = "Doe"
        };

        // Act
        var result = _mapper.Map<UserAuthDto>(user);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.FirstName, result.FirstName);
        Assert.Equal(user.LastName, result.LastName);
    }

    [Fact]
    public void UserCreateDto_ToUser_ShouldMapCorrectly()
    {
        // Arrange
        var createDto = new UserCreateDto
        {
            Email = "newuser@example.com",
            Password = "password123",
            FirstName = "Jane",
            LastName = "Smith",
            PhoneNumber = "+9876543210",
            CreatedBy = "admin"
        };

        // Act
        var result = _mapper.Map<User>(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createDto.Email, result.Email);
        Assert.Equal(createDto.FirstName, result.FirstName);
        Assert.Equal(createDto.LastName, result.LastName);
        Assert.Equal(createDto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(createDto.CreatedBy, result.CreatedBy);
        Assert.True(result.IsActive);
        Assert.NotEqual(default(DateTime), result.CreatedDate);
        Assert.NotNull(result.PasswordHash);
        Assert.NotEmpty(result.PasswordHash);
    }

    [Fact]
    public void UserUpdateDto_ToUser_ShouldMapCorrectly()
    {
        // Arrange
        var updateDto = new UserUpdateDto
        {
            FirstName = "Updated",
            LastName = "User",
            PhoneNumber = "+1111111111",
            ModifiedBy = "editor"
        };

        // Act
        var result = _mapper.Map<User>(updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updateDto.FirstName, result.FirstName);
        Assert.Equal(updateDto.LastName, result.LastName);
        Assert.Equal(updateDto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(updateDto.ModifiedBy, result.ModifiedBy);
    }

    [Fact]
    public void BookingCreateDto_ToBooking_ShouldMapCorrectly()
    {
        // Arrange
        var createDto = new BookingCreateDto
        {
            UserId = 1,
            TourId = 2,
            BookingDate = DateTime.UtcNow,
            NumberOfPeople = 4,
            TotalPrice = 4999.99m,
            CreatedBy = "user"
        };

        // Act
        var result = _mapper.Map<Booking>(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createDto.UserId, result.UserId);
        Assert.Equal(createDto.TourId, result.TourId);
        Assert.Equal(createDto.NumberOfPeople, result.NumberOfPeople);
        Assert.Equal(createDto.TotalPrice, result.TotalPrice);
        Assert.Equal(createDto.CreatedBy, result.CreatedBy);
        Assert.True(result.IsActive);
        Assert.Equal("Pending", result.Status);
        Assert.NotEqual(default(DateTime), result.CreatedDate);
    }

    [Fact]
    public void BookingUpdateDto_ToBooking_ShouldMapCorrectly()
    {
        // Arrange
        var updateDto = new BookingUpdateDto
        {
            BookingDate = DateTime.UtcNow,
            NumberOfPeople = 6,
            TotalPrice = 5999.99m,
            Status = "Confirmed",
            ModifiedBy = "admin"
        };

        // Act
        var result = _mapper.Map<Booking>(updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updateDto.NumberOfPeople, result.NumberOfPeople);
        Assert.Equal(updateDto.TotalPrice, result.TotalPrice);
        Assert.Equal(updateDto.Status, result.Status);
        Assert.Equal(updateDto.ModifiedBy, result.ModifiedBy);
    }

    [Fact]
    public void MappingProfile_ShouldIgnoreIdFieldsInCreateDtos()
    {
        // Arrange
        var tourCreateDto = new TourCreateDto { TourName = "Test" };
        var userCreateDto = new UserCreateDto { Email = "test@test.com", Password = "pass" };
        var bookingCreateDto = new BookingCreateDto { UserId = 1, TourId = 1 };

        // Act
        var tour = _mapper.Map<Tour>(tourCreateDto);
        var user = _mapper.Map<User>(userCreateDto);
        var booking = _mapper.Map<Booking>(bookingCreateDto);

        // Assert
        Assert.Equal(0, tour.Id);
        Assert.Equal(0, user.Id);
        Assert.Equal(0, booking.Id);
    }

    [Fact]
    public void MappingProfile_ShouldSetDefaultValuesForCreateDtos()
    {
        // Arrange
        var tourCreateDto = new TourCreateDto { TourName = "Test" };
        var userCreateDto = new UserCreateDto { Email = "test@test.com", Password = "pass" };
        var bookingCreateDto = new BookingCreateDto { UserId = 1, TourId = 1 };

        // Act
        var tour = _mapper.Map<Tour>(tourCreateDto);
        var user = _mapper.Map<User>(userCreateDto);
        var booking = _mapper.Map<Booking>(bookingCreateDto);

        // Assert
        Assert.True(tour.IsActive);
        Assert.True(user.IsActive);
        Assert.True(booking.IsActive);
        Assert.NotEqual(default(DateTime), tour.CreatedDate);
        Assert.NotEqual(default(DateTime), user.CreatedDate);
        Assert.NotEqual(default(DateTime), booking.CreatedDate);
    }

    [Fact]
    public void MappingProfile_ShouldHashPasswordInUserCreateDto()
    {
        // Arrange
        var password = "mySecurePassword123";
        var createDto = new UserCreateDto
        {
            Email = "test@test.com",
            Password = password
        };

        // Act
        var user = _mapper.Map<User>(createDto);

        // Assert
        Assert.NotNull(user.PasswordHash);
        Assert.NotEmpty(user.PasswordHash);
        Assert.NotEqual(password, user.PasswordHash);
        var expectedHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        Assert.Equal(expectedHash, user.PasswordHash);
    }
}
