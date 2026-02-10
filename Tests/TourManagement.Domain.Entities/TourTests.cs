using Xunit;
using System;
using System.Collections.Generic;
using TourManagement.Domain.Entities;

namespace TourManagement.Domain.Entities.Tests;

public class TourTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var tour = new Tour();

        // Assert
        Assert.NotNull(tour);
        Assert.Equal(0, tour.Id);
        Assert.Equal(string.Empty, tour.TourName);
        Assert.Equal(string.Empty, tour.Place);
        Assert.Equal(0, tour.Days);
        Assert.Equal(0m, tour.Price);
        Assert.Equal(string.Empty, tour.Locations);
        Assert.Equal(string.Empty, tour.TourInfo);
        Assert.Null(tour.PicturePath);
        Assert.Equal(default(DateTime), tour.CreatedDate);
        Assert.Null(tour.ModifiedDate);
        Assert.False(tour.IsActive);
        Assert.Equal(string.Empty, tour.CreatedBy);
        Assert.Null(tour.ModifiedBy);
        Assert.NotNull(tour.Bookings);
        Assert.Empty(tour.Bookings);
    }

    [Fact]
    public void Id_ShouldSetAndGetValue()
    {
        // Arrange
        var tour = new Tour();
        var expectedId = 123;

        // Act
        tour.Id = expectedId;

        // Assert
        Assert.Equal(expectedId, tour.Id);
    }

    [Fact]
    public void TourName_ShouldSetAndGetValue()
    {
        // Arrange
        var tour = new Tour();
        var expectedName = "Amazing Europe Tour";

        // Act
        tour.TourName = expectedName;

        // Assert
        Assert.Equal(expectedName, tour.TourName);
    }

    [Fact]
    public void Place_ShouldSetAndGetValue()
    {
        // Arrange
        var tour = new Tour();
        var expectedPlace = "Europe";

        // Act
        tour.Place = expectedPlace;

        // Assert
        Assert.Equal(expectedPlace, tour.Place);
    }

    [Fact]
    public void Days_ShouldSetAndGetValue()
    {
        // Arrange
        var tour = new Tour();
        var expectedDays = 7;

        // Act
        tour.Days = expectedDays;

        // Assert
        Assert.Equal(expectedDays, tour.Days);
    }

    [Fact]
    public void Days_ShouldHandleNegativeValue()
    {
        // Arrange
        var tour = new Tour();
        var negativeValue = -5;

        // Act
        tour.Days = negativeValue;

        // Assert
        Assert.Equal(negativeValue, tour.Days);
    }

    [Fact]
    public void Price_ShouldSetAndGetValue()
    {
        // Arrange
        var tour = new Tour();
        var expectedPrice = 1299.99m;

        // Act
        tour.Price = expectedPrice;

        // Assert
        Assert.Equal(expectedPrice, tour.Price);
    }

    [Fact]
    public void Price_ShouldHandleZeroValue()
    {
        // Arrange
        var tour = new Tour();

        // Act
        tour.Price = 0m;

        // Assert
        Assert.Equal(0m, tour.Price);
    }

    [Fact]
    public void Locations_ShouldSetAndGetValue()
    {
        // Arrange
        var tour = new Tour();
        var expectedLocations = "Paris, London, Rome";

        // Act
        tour.Locations = expectedLocations;

        // Assert
        Assert.Equal(expectedLocations, tour.Locations);
    }

    [Fact]
    public void TourInfo_ShouldSetAndGetValue()
    {
        // Arrange
        var tour = new Tour();
        var expectedInfo = "Explore the best of Europe";

        // Act
        tour.TourInfo = expectedInfo;

        // Assert
        Assert.Equal(expectedInfo, tour.TourInfo);
    }

    [Fact]
    public void PicturePath_ShouldSetAndGetValue()
    {
        // Arrange
        var tour = new Tour();
        var expectedPath = "/images/tour123.jpg";

        // Act
        tour.PicturePath = expectedPath;

        // Assert
        Assert.Equal(expectedPath, tour.PicturePath);
    }

    [Fact]
    public void PicturePath_ShouldAcceptNull()
    {
        // Arrange
        var tour = new Tour();

        // Act
        tour.PicturePath = null;

        // Assert
        Assert.Null(tour.PicturePath);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetValue()
    {
        // Arrange
        var tour = new Tour();
        var expectedDate = new DateTime(2024, 1, 15);

        // Act
        tour.CreatedDate = expectedDate;

        // Assert
        Assert.Equal(expectedDate, tour.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetValue()
    {
        // Arrange
        var tour = new Tour();
        var expectedDate = new DateTime(2024, 2, 20);

        // Act
        tour.ModifiedDate = expectedDate;

        // Assert
        Assert.Equal(expectedDate, tour.ModifiedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldAcceptNull()
    {
        // Arrange
        var tour = new Tour();

        // Act
        tour.ModifiedDate = null;

        // Assert
        Assert.Null(tour.ModifiedDate);
    }

    [Fact]
    public void IsActive_ShouldSetAndGetValue()
    {
        // Arrange
        var tour = new Tour();

        // Act
        tour.IsActive = true;

        // Assert
        Assert.True(tour.IsActive);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetValue()
    {
        // Arrange
        var tour = new Tour();
        var expectedUser = "admin@example.com";

        // Act
        tour.CreatedBy = expectedUser;

        // Assert
        Assert.Equal(expectedUser, tour.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetValue()
    {
        // Arrange
        var tour = new Tour();
        var expectedUser = "editor@example.com";

        // Act
        tour.ModifiedBy = expectedUser;

        // Assert
        Assert.Equal(expectedUser, tour.ModifiedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldAcceptNull()
    {
        // Arrange
        var tour = new Tour();

        // Act
        tour.ModifiedBy = null;

        // Assert
        Assert.Null(tour.ModifiedBy);
    }

    [Fact]
    public void Bookings_ShouldInitializeAsEmptyCollection()
    {
        // Arrange & Act
        var tour = new Tour();

        // Assert
        Assert.NotNull(tour.Bookings);
        Assert.Empty(tour.Bookings);
        Assert.IsAssignableFrom<ICollection<Booking>>(tour.Bookings);
    }

    [Fact]
    public void Bookings_ShouldAllowAddingItems()
    {
        // Arrange
        var tour = new Tour();
        var booking = new Booking { Id = 1, TourId = tour.Id };

        // Act
        tour.Bookings.Add(booking);

        // Assert
        Assert.Single(tour.Bookings);
        Assert.Contains(booking, tour.Bookings);
    }

    [Fact]
    public void Tour_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var tour = new Tour();
        var expectedId = 100;
        var expectedName = "Grand Tour";
        var expectedPlace = "Asia";
        var expectedDays = 14;
        var expectedPrice = 2500.50m;
        var expectedLocations = "Tokyo, Seoul, Bangkok";
        var expectedInfo = "Discover Asia";
        var expectedPicture = "/images/asia.jpg";
        var expectedCreatedDate = DateTime.UtcNow;
        var expectedModifiedDate = DateTime.UtcNow.AddDays(5);
        var expectedIsActive = true;
        var expectedCreatedBy = "admin";
        var expectedModifiedBy = "editor";

        // Act
        tour.Id = expectedId;
        tour.TourName = expectedName;
        tour.Place = expectedPlace;
        tour.Days = expectedDays;
        tour.Price = expectedPrice;
        tour.Locations = expectedLocations;
        tour.TourInfo = expectedInfo;
        tour.PicturePath = expectedPicture;
        tour.CreatedDate = expectedCreatedDate;
        tour.ModifiedDate = expectedModifiedDate;
        tour.IsActive = expectedIsActive;
        tour.CreatedBy = expectedCreatedBy;
        tour.ModifiedBy = expectedModifiedBy;

        // Assert
        Assert.Equal(expectedId, tour.Id);
        Assert.Equal(expectedName, tour.TourName);
        Assert.Equal(expectedPlace, tour.Place);
        Assert.Equal(expectedDays, tour.Days);
        Assert.Equal(expectedPrice, tour.Price);
        Assert.Equal(expectedLocations, tour.Locations);
        Assert.Equal(expectedInfo, tour.TourInfo);
        Assert.Equal(expectedPicture, tour.PicturePath);
        Assert.Equal(expectedCreatedDate, tour.CreatedDate);
        Assert.Equal(expectedModifiedDate, tour.ModifiedDate);
        Assert.Equal(expectedIsActive, tour.IsActive);
        Assert.Equal(expectedCreatedBy, tour.CreatedBy);
        Assert.Equal(expectedModifiedBy, tour.ModifiedBy);
    }
}
