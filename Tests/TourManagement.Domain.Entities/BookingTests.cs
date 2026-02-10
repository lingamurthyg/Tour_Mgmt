using Xunit;
using System;
using TourManagement.Domain.Entities;

namespace TourManagement.Domain.Entities.Tests;

public class BookingTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var booking = new Booking();

        // Assert
        Assert.NotNull(booking);
        Assert.Equal(0, booking.Id);
        Assert.Equal(0, booking.UserId);
        Assert.Equal(0, booking.TourId);
        Assert.Equal(default(DateTime), booking.BookingDate);
        Assert.Equal(0, booking.NumberOfPeople);
        Assert.Equal(0m, booking.TotalPrice);
        Assert.Equal("Pending", booking.Status);
        Assert.Equal(default(DateTime), booking.CreatedDate);
        Assert.Null(booking.ModifiedDate);
        Assert.False(booking.IsActive);
        Assert.Equal(string.Empty, booking.CreatedBy);
        Assert.Null(booking.ModifiedBy);
    }

    [Fact]
    public void Id_ShouldSetAndGetValue()
    {
        // Arrange
        var booking = new Booking();
        var expectedId = 555;

        // Act
        booking.Id = expectedId;

        // Assert
        Assert.Equal(expectedId, booking.Id);
    }

    [Fact]
    public void UserId_ShouldSetAndGetValue()
    {
        // Arrange
        var booking = new Booking();
        var expectedUserId = 100;

        // Act
        booking.UserId = expectedUserId;

        // Assert
        Assert.Equal(expectedUserId, booking.UserId);
    }

    [Fact]
    public void TourId_ShouldSetAndGetValue()
    {
        // Arrange
        var booking = new Booking();
        var expectedTourId = 200;

        // Act
        booking.TourId = expectedTourId;

        // Assert
        Assert.Equal(expectedTourId, booking.TourId);
    }

    [Fact]
    public void BookingDate_ShouldSetAndGetValue()
    {
        // Arrange
        var booking = new Booking();
        var expectedDate = new DateTime(2024, 7, 15);

        // Act
        booking.BookingDate = expectedDate;

        // Assert
        Assert.Equal(expectedDate, booking.BookingDate);
    }

    [Fact]
    public void NumberOfPeople_ShouldSetAndGetValue()
    {
        // Arrange
        var booking = new Booking();
        var expectedNumber = 4;

        // Act
        booking.NumberOfPeople = expectedNumber;

        // Assert
        Assert.Equal(expectedNumber, booking.NumberOfPeople);
    }

    [Fact]
    public void NumberOfPeople_ShouldHandleZero()
    {
        // Arrange
        var booking = new Booking();

        // Act
        booking.NumberOfPeople = 0;

        // Assert
        Assert.Equal(0, booking.NumberOfPeople);
    }

    [Fact]
    public void NumberOfPeople_ShouldHandleNegativeValue()
    {
        // Arrange
        var booking = new Booking();

        // Act
        booking.NumberOfPeople = -1;

        // Assert
        Assert.Equal(-1, booking.NumberOfPeople);
    }

    [Fact]
    public void TotalPrice_ShouldSetAndGetValue()
    {
        // Arrange
        var booking = new Booking();
        var expectedPrice = 5199.99m;

        // Act
        booking.TotalPrice = expectedPrice;

        // Assert
        Assert.Equal(expectedPrice, booking.TotalPrice);
    }

    [Fact]
    public void TotalPrice_ShouldHandleZero()
    {
        // Arrange
        var booking = new Booking();

        // Act
        booking.TotalPrice = 0m;

        // Assert
        Assert.Equal(0m, booking.TotalPrice);
    }

    [Fact]
    public void Status_ShouldSetAndGetValue()
    {
        // Arrange
        var booking = new Booking();
        var expectedStatus = "Confirmed";

        // Act
        booking.Status = expectedStatus;

        // Assert
        Assert.Equal(expectedStatus, booking.Status);
    }

    [Fact]
    public void Status_ShouldDefaultToPending()
    {
        // Arrange & Act
        var booking = new Booking();

        // Assert
        Assert.Equal("Pending", booking.Status);
    }

    [Fact]
    public void Status_ShouldHandleCancelled()
    {
        // Arrange
        var booking = new Booking();

        // Act
        booking.Status = "Cancelled";

        // Assert
        Assert.Equal("Cancelled", booking.Status);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetValue()
    {
        // Arrange
        var booking = new Booking();
        var expectedDate = new DateTime(2024, 1, 1);

        // Act
        booking.CreatedDate = expectedDate;

        // Assert
        Assert.Equal(expectedDate, booking.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetValue()
    {
        // Arrange
        var booking = new Booking();
        var expectedDate = new DateTime(2024, 2, 15);

        // Act
        booking.ModifiedDate = expectedDate;

        // Assert
        Assert.Equal(expectedDate, booking.ModifiedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldAcceptNull()
    {
        // Arrange
        var booking = new Booking();

        // Act
        booking.ModifiedDate = null;

        // Assert
        Assert.Null(booking.ModifiedDate);
    }

    [Fact]
    public void IsActive_ShouldSetToTrue()
    {
        // Arrange
        var booking = new Booking();

        // Act
        booking.IsActive = true;

        // Assert
        Assert.True(booking.IsActive);
    }

    [Fact]
    public void IsActive_ShouldSetToFalse()
    {
        // Arrange
        var booking = new Booking();

        // Act
        booking.IsActive = false;

        // Assert
        Assert.False(booking.IsActive);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetValue()
    {
        // Arrange
        var booking = new Booking();
        var expectedCreator = "user@example.com";

        // Act
        booking.CreatedBy = expectedCreator;

        // Assert
        Assert.Equal(expectedCreator, booking.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetValue()
    {
        // Arrange
        var booking = new Booking();
        var expectedModifier = "admin@example.com";

        // Act
        booking.ModifiedBy = expectedModifier;

        // Assert
        Assert.Equal(expectedModifier, booking.ModifiedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldAcceptNull()
    {
        // Arrange
        var booking = new Booking();

        // Act
        booking.ModifiedBy = null;

        // Assert
        Assert.Null(booking.ModifiedBy);
    }

    [Fact]
    public void User_ShouldSetAndGetNavigationProperty()
    {
        // Arrange
        var booking = new Booking();
        var user = new User { Id = 10, Email = "test@test.com" };

        // Act
        booking.User = user;

        // Assert
        Assert.NotNull(booking.User);
        Assert.Equal(user.Id, booking.User.Id);
        Assert.Equal(user.Email, booking.User.Email);
    }

    [Fact]
    public void Tour_ShouldSetAndGetNavigationProperty()
    {
        // Arrange
        var booking = new Booking();
        var tour = new Tour { Id = 20, TourName = "Paris Tour" };

        // Act
        booking.Tour = tour;

        // Assert
        Assert.NotNull(booking.Tour);
        Assert.Equal(tour.Id, booking.Tour.Id);
        Assert.Equal(tour.TourName, booking.Tour.TourName);
    }

    [Fact]
    public void Booking_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var booking = new Booking();
        var user = new User { Id = 50 };
        var tour = new Tour { Id = 60 };
        var expectedId = 777;
        var expectedUserId = 50;
        var expectedTourId = 60;
        var expectedBookingDate = DateTime.UtcNow;
        var expectedNumberOfPeople = 3;
        var expectedTotalPrice = 3999.99m;
        var expectedStatus = "Confirmed";
        var expectedCreatedDate = DateTime.UtcNow;
        var expectedModifiedDate = DateTime.UtcNow.AddHours(5);
        var expectedIsActive = true;
        var expectedCreatedBy = "customer@example.com";
        var expectedModifiedBy = "support@example.com";

        // Act
        booking.Id = expectedId;
        booking.UserId = expectedUserId;
        booking.TourId = expectedTourId;
        booking.BookingDate = expectedBookingDate;
        booking.NumberOfPeople = expectedNumberOfPeople;
        booking.TotalPrice = expectedTotalPrice;
        booking.Status = expectedStatus;
        booking.CreatedDate = expectedCreatedDate;
        booking.ModifiedDate = expectedModifiedDate;
        booking.IsActive = expectedIsActive;
        booking.CreatedBy = expectedCreatedBy;
        booking.ModifiedBy = expectedModifiedBy;
        booking.User = user;
        booking.Tour = tour;

        // Assert
        Assert.Equal(expectedId, booking.Id);
        Assert.Equal(expectedUserId, booking.UserId);
        Assert.Equal(expectedTourId, booking.TourId);
        Assert.Equal(expectedBookingDate, booking.BookingDate);
        Assert.Equal(expectedNumberOfPeople, booking.NumberOfPeople);
        Assert.Equal(expectedTotalPrice, booking.TotalPrice);
        Assert.Equal(expectedStatus, booking.Status);
        Assert.Equal(expectedCreatedDate, booking.CreatedDate);
        Assert.Equal(expectedModifiedDate, booking.ModifiedDate);
        Assert.Equal(expectedIsActive, booking.IsActive);
        Assert.Equal(expectedCreatedBy, booking.CreatedBy);
        Assert.Equal(expectedModifiedBy, booking.ModifiedBy);
        Assert.NotNull(booking.User);
        Assert.NotNull(booking.Tour);
    }
}
