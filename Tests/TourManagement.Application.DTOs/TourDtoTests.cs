using Xunit;
using System;
using TourManagement.Application.DTOs;

namespace TourManagement.Application.DTOs.Tests;

public class TourDtoTests
{
    [Fact]
    public void TourDto_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new TourDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(0, dto.Id);
        Assert.Equal(string.Empty, dto.TourName);
        Assert.Equal(string.Empty, dto.Place);
        Assert.Equal(0, dto.Days);
        Assert.Equal(0m, dto.Price);
        Assert.Equal(string.Empty, dto.Locations);
        Assert.Equal(string.Empty, dto.TourInfo);
        Assert.Null(dto.PicturePath);
        Assert.Equal(default(DateTime), dto.CreatedDate);
        Assert.False(dto.IsActive);
    }

    [Fact]
    public void TourDto_ShouldSetAndGetAllProperties()
    {
        // Arrange
        var dto = new TourDto();
        var expectedId = 10;
        var expectedName = "European Adventure";
        var expectedPlace = "Europe";
        var expectedDays = 10;
        var expectedPrice = 2500.50m;
        var expectedLocations = "Paris, Rome, Berlin";
        var expectedInfo = "A wonderful European tour";
        var expectedPicture = "/images/europe.jpg";
        var expectedCreatedDate = DateTime.UtcNow;
        var expectedIsActive = true;

        // Act
        dto.Id = expectedId;
        dto.TourName = expectedName;
        dto.Place = expectedPlace;
        dto.Days = expectedDays;
        dto.Price = expectedPrice;
        dto.Locations = expectedLocations;
        dto.TourInfo = expectedInfo;
        dto.PicturePath = expectedPicture;
        dto.CreatedDate = expectedCreatedDate;
        dto.IsActive = expectedIsActive;

        // Assert
        Assert.Equal(expectedId, dto.Id);
        Assert.Equal(expectedName, dto.TourName);
        Assert.Equal(expectedPlace, dto.Place);
        Assert.Equal(expectedDays, dto.Days);
        Assert.Equal(expectedPrice, dto.Price);
        Assert.Equal(expectedLocations, dto.Locations);
        Assert.Equal(expectedInfo, dto.TourInfo);
        Assert.Equal(expectedPicture, dto.PicturePath);
        Assert.Equal(expectedCreatedDate, dto.CreatedDate);
        Assert.Equal(expectedIsActive, dto.IsActive);
    }

    [Fact]
    public void TourDto_PicturePath_ShouldAcceptNull()
    {
        // Arrange
        var dto = new TourDto();

        // Act
        dto.PicturePath = null;

        // Assert
        Assert.Null(dto.PicturePath);
    }
}

public class TourCreateDtoTests
{
    [Fact]
    public void TourCreateDto_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new TourCreateDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.TourName);
        Assert.Equal(string.Empty, dto.Place);
        Assert.Equal(0, dto.Days);
        Assert.Equal(0m, dto.Price);
        Assert.Equal(string.Empty, dto.Locations);
        Assert.Equal(string.Empty, dto.TourInfo);
        Assert.Null(dto.PicturePath);
        Assert.Equal("System", dto.CreatedBy);
    }

    [Fact]
    public void TourCreateDto_ShouldSetAndGetAllProperties()
    {
        // Arrange
        var dto = new TourCreateDto();
        var expectedName = "Asian Exploration";
        var expectedPlace = "Asia";
        var expectedDays = 14;
        var expectedPrice = 3500.00m;
        var expectedLocations = "Tokyo, Bangkok, Singapore";
        var expectedInfo = "Explore the best of Asia";
        var expectedPicture = "/images/asia.jpg";
        var expectedCreatedBy = "admin@example.com";

        // Act
        dto.TourName = expectedName;
        dto.Place = expectedPlace;
        dto.Days = expectedDays;
        dto.Price = expectedPrice;
        dto.Locations = expectedLocations;
        dto.TourInfo = expectedInfo;
        dto.PicturePath = expectedPicture;
        dto.CreatedBy = expectedCreatedBy;

        // Assert
        Assert.Equal(expectedName, dto.TourName);
        Assert.Equal(expectedPlace, dto.Place);
        Assert.Equal(expectedDays, dto.Days);
        Assert.Equal(expectedPrice, dto.Price);
        Assert.Equal(expectedLocations, dto.Locations);
        Assert.Equal(expectedInfo, dto.TourInfo);
        Assert.Equal(expectedPicture, dto.PicturePath);
        Assert.Equal(expectedCreatedBy, dto.CreatedBy);
    }

    [Fact]
    public void TourCreateDto_CreatedBy_ShouldDefaultToSystem()
    {
        // Arrange & Act
        var dto = new TourCreateDto();

        // Assert
        Assert.Equal("System", dto.CreatedBy);
    }

    [Fact]
    public void TourCreateDto_PicturePath_ShouldAcceptNull()
    {
        // Arrange
        var dto = new TourCreateDto();

        // Act
        dto.PicturePath = null;

        // Assert
        Assert.Null(dto.PicturePath);
    }

    [Fact]
    public void TourCreateDto_ShouldHandleZeroDays()
    {
        // Arrange
        var dto = new TourCreateDto();

        // Act
        dto.Days = 0;

        // Assert
        Assert.Equal(0, dto.Days);
    }

    [Fact]
    public void TourCreateDto_ShouldHandleZeroPrice()
    {
        // Arrange
        var dto = new TourCreateDto();

        // Act
        dto.Price = 0m;

        // Assert
        Assert.Equal(0m, dto.Price);
    }
}

public class TourUpdateDtoTests
{
    [Fact]
    public void TourUpdateDto_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new TourUpdateDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(string.Empty, dto.TourName);
        Assert.Equal(string.Empty, dto.Place);
        Assert.Equal(0, dto.Days);
        Assert.Equal(0m, dto.Price);
        Assert.Equal(string.Empty, dto.Locations);
        Assert.Equal(string.Empty, dto.TourInfo);
        Assert.Null(dto.PicturePath);
        Assert.Equal("System", dto.ModifiedBy);
    }

    [Fact]
    public void TourUpdateDto_ShouldSetAndGetAllProperties()
    {
        // Arrange
        var dto = new TourUpdateDto();
        var expectedName = "Updated Tour";
        var expectedPlace = "Updated Place";
        var expectedDays = 20;
        var expectedPrice = 4500.75m;
        var expectedLocations = "New York, Los Angeles";
        var expectedInfo = "Updated tour information";
        var expectedPicture = "/images/updated.jpg";
        var expectedModifiedBy = "editor@example.com";

        // Act
        dto.TourName = expectedName;
        dto.Place = expectedPlace;
        dto.Days = expectedDays;
        dto.Price = expectedPrice;
        dto.Locations = expectedLocations;
        dto.TourInfo = expectedInfo;
        dto.PicturePath = expectedPicture;
        dto.ModifiedBy = expectedModifiedBy;

        // Assert
        Assert.Equal(expectedName, dto.TourName);
        Assert.Equal(expectedPlace, dto.Place);
        Assert.Equal(expectedDays, dto.Days);
        Assert.Equal(expectedPrice, dto.Price);
        Assert.Equal(expectedLocations, dto.Locations);
        Assert.Equal(expectedInfo, dto.TourInfo);
        Assert.Equal(expectedPicture, dto.PicturePath);
        Assert.Equal(expectedModifiedBy, dto.ModifiedBy);
    }

    [Fact]
    public void TourUpdateDto_ModifiedBy_ShouldDefaultToSystem()
    {
        // Arrange & Act
        var dto = new TourUpdateDto();

        // Assert
        Assert.Equal("System", dto.ModifiedBy);
    }

    [Fact]
    public void TourUpdateDto_PicturePath_ShouldAcceptNull()
    {
        // Arrange
        var dto = new TourUpdateDto();

        // Act
        dto.PicturePath = null;

        // Assert
        Assert.Null(dto.PicturePath);
    }

    [Fact]
    public void TourUpdateDto_ShouldHandleNegativeDays()
    {
        // Arrange
        var dto = new TourUpdateDto();

        // Act
        dto.Days = -5;

        // Assert
        Assert.Equal(-5, dto.Days);
    }

    [Fact]
    public void TourUpdateDto_ShouldHandleNegativePrice()
    {
        // Arrange
        var dto = new TourUpdateDto();

        // Act
        dto.Price = -100m;

        // Assert
        Assert.Equal(-100m, dto.Price);
    }
}
