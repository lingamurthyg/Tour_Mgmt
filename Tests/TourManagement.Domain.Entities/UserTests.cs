using Xunit;
using System;
using System.Collections.Generic;
using TourManagement.Domain.Entities;

namespace TourManagement.Domain.Entities.Tests;

public class UserTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var user = new User();

        // Assert
        Assert.NotNull(user);
        Assert.Equal(0, user.Id);
        Assert.Equal(string.Empty, user.Email);
        Assert.Equal(string.Empty, user.PasswordHash);
        Assert.Equal(string.Empty, user.FirstName);
        Assert.Equal(string.Empty, user.LastName);
        Assert.Null(user.PhoneNumber);
        Assert.Equal(default(DateTime), user.CreatedDate);
        Assert.Null(user.ModifiedDate);
        Assert.False(user.IsActive);
        Assert.Equal(string.Empty, user.CreatedBy);
        Assert.Null(user.ModifiedBy);
        Assert.NotNull(user.Bookings);
        Assert.Empty(user.Bookings);
    }

    [Fact]
    public void Id_ShouldSetAndGetValue()
    {
        // Arrange
        var user = new User();
        var expectedId = 42;

        // Act
        user.Id = expectedId;

        // Assert
        Assert.Equal(expectedId, user.Id);
    }

    [Fact]
    public void Email_ShouldSetAndGetValue()
    {
        // Arrange
        var user = new User();
        var expectedEmail = "user@example.com";

        // Act
        user.Email = expectedEmail;

        // Assert
        Assert.Equal(expectedEmail, user.Email);
    }

    [Fact]
    public void Email_ShouldHandleEmptyString()
    {
        // Arrange
        var user = new User();

        // Act
        user.Email = string.Empty;

        // Assert
        Assert.Equal(string.Empty, user.Email);
    }

    [Fact]
    public void PasswordHash_ShouldSetAndGetValue()
    {
        // Arrange
        var user = new User();
        var expectedHash = "hashed_password_123";

        // Act
        user.PasswordHash = expectedHash;

        // Assert
        Assert.Equal(expectedHash, user.PasswordHash);
    }

    [Fact]
    public void FirstName_ShouldSetAndGetValue()
    {
        // Arrange
        var user = new User();
        var expectedFirstName = "John";

        // Act
        user.FirstName = expectedFirstName;

        // Assert
        Assert.Equal(expectedFirstName, user.FirstName);
    }

    [Fact]
    public void LastName_ShouldSetAndGetValue()
    {
        // Arrange
        var user = new User();
        var expectedLastName = "Doe";

        // Act
        user.LastName = expectedLastName;

        // Assert
        Assert.Equal(expectedLastName, user.LastName);
    }

    [Fact]
    public void PhoneNumber_ShouldSetAndGetValue()
    {
        // Arrange
        var user = new User();
        var expectedPhone = "+1234567890";

        // Act
        user.PhoneNumber = expectedPhone;

        // Assert
        Assert.Equal(expectedPhone, user.PhoneNumber);
    }

    [Fact]
    public void PhoneNumber_ShouldAcceptNull()
    {
        // Arrange
        var user = new User();

        // Act
        user.PhoneNumber = null;

        // Assert
        Assert.Null(user.PhoneNumber);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetValue()
    {
        // Arrange
        var user = new User();
        var expectedDate = new DateTime(2024, 5, 15);

        // Act
        user.CreatedDate = expectedDate;

        // Assert
        Assert.Equal(expectedDate, user.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetValue()
    {
        // Arrange
        var user = new User();
        var expectedDate = new DateTime(2024, 6, 20);

        // Act
        user.ModifiedDate = expectedDate;

        // Assert
        Assert.Equal(expectedDate, user.ModifiedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldAcceptNull()
    {
        // Arrange
        var user = new User();

        // Act
        user.ModifiedDate = null;

        // Assert
        Assert.Null(user.ModifiedDate);
    }

    [Fact]
    public void IsActive_ShouldSetToTrue()
    {
        // Arrange
        var user = new User();

        // Act
        user.IsActive = true;

        // Assert
        Assert.True(user.IsActive);
    }

    [Fact]
    public void IsActive_ShouldSetToFalse()
    {
        // Arrange
        var user = new User();

        // Act
        user.IsActive = false;

        // Assert
        Assert.False(user.IsActive);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetValue()
    {
        // Arrange
        var user = new User();
        var expectedCreator = "admin@example.com";

        // Act
        user.CreatedBy = expectedCreator;

        // Assert
        Assert.Equal(expectedCreator, user.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetValue()
    {
        // Arrange
        var user = new User();
        var expectedModifier = "editor@example.com";

        // Act
        user.ModifiedBy = expectedModifier;

        // Assert
        Assert.Equal(expectedModifier, user.ModifiedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldAcceptNull()
    {
        // Arrange
        var user = new User();

        // Act
        user.ModifiedBy = null;

        // Assert
        Assert.Null(user.ModifiedBy);
    }

    [Fact]
    public void Bookings_ShouldInitializeAsEmptyCollection()
    {
        // Arrange & Act
        var user = new User();

        // Assert
        Assert.NotNull(user.Bookings);
        Assert.Empty(user.Bookings);
        Assert.IsAssignableFrom<ICollection<Booking>>(user.Bookings);
    }

    [Fact]
    public void Bookings_ShouldAllowAddingItems()
    {
        // Arrange
        var user = new User();
        var booking = new Booking { Id = 1, UserId = user.Id };

        // Act
        user.Bookings.Add(booking);

        // Assert
        Assert.Single(user.Bookings);
        Assert.Contains(booking, user.Bookings);
    }

    [Fact]
    public void User_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var user = new User();
        var expectedId = 999;
        var expectedEmail = "test@test.com";
        var expectedPasswordHash = "hash123";
        var expectedFirstName = "Jane";
        var expectedLastName = "Smith";
        var expectedPhone = "+9876543210";
        var expectedCreatedDate = DateTime.UtcNow;
        var expectedModifiedDate = DateTime.UtcNow.AddDays(10);
        var expectedIsActive = true;
        var expectedCreatedBy = "system";
        var expectedModifiedBy = "admin";

        // Act
        user.Id = expectedId;
        user.Email = expectedEmail;
        user.PasswordHash = expectedPasswordHash;
        user.FirstName = expectedFirstName;
        user.LastName = expectedLastName;
        user.PhoneNumber = expectedPhone;
        user.CreatedDate = expectedCreatedDate;
        user.ModifiedDate = expectedModifiedDate;
        user.IsActive = expectedIsActive;
        user.CreatedBy = expectedCreatedBy;
        user.ModifiedBy = expectedModifiedBy;

        // Assert
        Assert.Equal(expectedId, user.Id);
        Assert.Equal(expectedEmail, user.Email);
        Assert.Equal(expectedPasswordHash, user.PasswordHash);
        Assert.Equal(expectedFirstName, user.FirstName);
        Assert.Equal(expectedLastName, user.LastName);
        Assert.Equal(expectedPhone, user.PhoneNumber);
        Assert.Equal(expectedCreatedDate, user.CreatedDate);
        Assert.Equal(expectedModifiedDate, user.ModifiedDate);
        Assert.Equal(expectedIsActive, user.IsActive);
        Assert.Equal(expectedCreatedBy, user.CreatedBy);
        Assert.Equal(expectedModifiedBy, user.ModifiedBy);
    }
}
