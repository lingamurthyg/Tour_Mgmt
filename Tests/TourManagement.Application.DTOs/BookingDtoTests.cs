using Xunit;
using System;
using TourManagement.Application.DTOs;

namespace TourManagement.Application.DTOs.Tests;

public class BookingDtoTests
{
    [Fact]
    public void BookingDto_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new BookingDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(0, dto.Id);
        Assert.Equal(0, dto.UserId);
        Assert.Equal(0, dto.TourId);
        Assert.Equal(string.Empty, dto.UserEmail);
        Assert.Equal(string.Empty, dto.TourName);
        Assert.Equal(default(DateTime), dto.BookingDate);
        Assert.Equal(0, dto.NumberOfPeople);
        Assert.Equal(0m, dto.TotalPrice);
        Assert.Equal(string.Empty, dto.Status);
        Assert.Equal(default(DateTime), dto.CreatedDate);
    }

    [Fact]
    public void BookingDto_ShouldSetAndGetAllProperties()
    {
        // Arrange
        var dto = new BookingDto();
        var expectedId = 123;
        var expectedUserId = 456;
        var expectedTourId = 789;
        var expectedUserEmail = "user@example.com";
        var expectedTourName = "Paris Tour";
        var expectedBookingDate = new DateTime(2024, 8, 15);
        var expectedNumberOfPeople = 3;
        var expectedTotalPrice = 3999.99m;
        var expectedStatus = "Confirmed";
        var expectedCreatedDate = DateTime.UtcNow;

        // Act
        dto.Id = expectedId;
        dto.UserId = expectedUserId;
        dto.TourId = expectedTourId;
        dto.UserEmail = expectedUserEmail;
        dto.TourName = expectedTourName;
        dto.BookingDate = expectedBookingDate;
        dto.NumberOfPeople = expectedNumberOfPeople;
        dto.TotalPrice = expectedTotalPrice;
        dto.Status = expectedStatus;
        dto.CreatedDate = expectedCreatedDate;

        // Assert
        Assert.Equal(expectedId, dto.Id);
        Assert.Equal(expectedUserId, dto.UserId);
        Assert.Equal(expectedTourId, dto.TourId);
        Assert.Equal(expectedUserEmail, dto.UserEmail);
        Assert.Equal(expectedTourName, dto.TourName);
        Assert.Equal(expectedBookingDate, dto.BookingDate);
        Assert.Equal(expectedNumberOfPeople, dto.NumberOfPeople);
        Assert.Equal(expectedTotalPrice, dto.TotalPrice);
        Assert.Equal(expectedStatus, dto.Status);
        Assert.Equal(expectedCreatedDate, dto.CreatedDate);
    }

    [Fact]
    public void BookingDto_ShouldHandleZeroValues()
    {
        // Arrange
        var dto = new BookingDto();

        // Act
        dto.NumberOfPeople = 0;
        dto.TotalPrice = 0m;

        // Assert
        Assert.Equal(0, dto.NumberOfPeople);
        Assert.Equal(0m, dto.TotalPrice);
    }
}

public class BookingCreateDtoTests
{
    [Fact]
    public void BookingCreateDto_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new BookingCreateDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(0, dto.UserId);
        Assert.Equal(0, dto.TourId);
        Assert.Equal(default(DateTime), dto.BookingDate);
        Assert.Equal(0, dto.NumberOfPeople);
        Assert.Equal(0m, dto.TotalPrice);
        Assert.Equal("System", dto.CreatedBy);
    }

    [Fact]
    public void BookingCreateDto_ShouldSetAndGetAllProperties()
    {
        // Arrange
        var dto = new BookingCreateDto();
        var expectedUserId = 111;
        var expectedTourId = 222;
        var expectedBookingDate = new DateTime(2024, 9, 20);
        var expectedNumberOfPeople = 5;
        var expectedTotalPrice = 6500.50m;
        var expectedCreatedBy = "customer@example.com";

        // Act
        dto.UserId = expectedUserId;
        dto.TourId = expectedTourId;
        dto.BookingDate = expectedBookingDate;
        dto.NumberOfPeople = expectedNumberOfPeople;
        dto.TotalPrice = expectedTotalPrice;
        dto.CreatedBy = expectedCreatedBy;

        // Assert
        Assert.Equal(expectedUserId, dto.UserId);
        Assert.Equal(expectedTourId, dto.TourId);
        Assert.Equal(expectedBookingDate, dto.BookingDate);
        Assert.Equal(expectedNumberOfPeople, dto.NumberOfPeople);
        Assert.Equal(expectedTotalPrice, dto.TotalPrice);
        Assert.Equal(expectedCreatedBy, dto.CreatedBy);
    }

    [Fact]
    public void BookingCreateDto_CreatedBy_ShouldDefaultToSystem()
    {
        // Arrange & Act
        var dto = new BookingCreateDto();

        // Assert
        Assert.Equal("System", dto.CreatedBy);
    }

    [Fact]
    public void BookingCreateDto_ShouldHandleNegativeValues()
    {
        // Arrange
        var dto = new BookingCreateDto();

        // Act
        dto.NumberOfPeople = -1;
        dto.TotalPrice = -100m;

        // Assert
        Assert.Equal(-1, dto.NumberOfPeople);
        Assert.Equal(-100m, dto.TotalPrice);
    }

    [Fact]
    public void BookingCreateDto_ShouldHandleFutureDates()
    {
        // Arrange
        var dto = new BookingCreateDto();
        var futureDate = DateTime.UtcNow.AddYears(1);

        // Act
        dto.BookingDate = futureDate;

        // Assert
        Assert.Equal(futureDate, dto.BookingDate);
    }

    [Fact]
    public void BookingCreateDto_ShouldHandlePastDates()
    {
        // Arrange
        var dto = new BookingCreateDto();
        var pastDate = DateTime.UtcNow.AddYears(-1);

        // Act
        dto.BookingDate = pastDate;

        // Assert
        Assert.Equal(pastDate, dto.BookingDate);
    }
}

public class BookingUpdateDtoTests
{
    [Fact]
    public void BookingUpdateDto_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new BookingUpdateDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(default(DateTime), dto.BookingDate);
        Assert.Equal(0, dto.NumberOfPeople);
        Assert.Equal(0m, dto.TotalPrice);
        Assert.Equal("Pending", dto.Status);
        Assert.Equal("System", dto.ModifiedBy);
    }

    [Fact]
    public void BookingUpdateDto_ShouldSetAndGetAllProperties()
    {
        // Arrange
        var dto = new BookingUpdateDto();
        var expectedBookingDate = new DateTime(2024, 10, 25);
        var expectedNumberOfPeople = 7;
        var expectedTotalPrice = 8999.99m;
        var expectedStatus = "Cancelled";
        var expectedModifiedBy = "admin@example.com";

        // Act
        dto.BookingDate = expectedBookingDate;
        dto.NumberOfPeople = expectedNumberOfPeople;
        dto.TotalPrice = expectedTotalPrice;
        dto.Status = expectedStatus;
        dto.ModifiedBy = expectedModifiedBy;

        // Assert
        Assert.Equal(expectedBookingDate, dto.BookingDate);
        Assert.Equal(expectedNumberOfPeople, dto.NumberOfPeople);
        Assert.Equal(expectedTotalPrice, dto.TotalPrice);
        Assert.Equal(expectedStatus, dto.Status);
        Assert.Equal(expectedModifiedBy, dto.ModifiedBy);
    }

    [Fact]
    public void BookingUpdateDto_Status_ShouldDefaultToPending()
    {
        // Arrange & Act
        var dto = new BookingUpdateDto();

        // Assert
        Assert.Equal("Pending", dto.Status);
    }

    [Fact]
    public void BookingUpdateDto_ModifiedBy_ShouldDefaultToSystem()
    {
        // Arrange & Act
        var dto = new BookingUpdateDto();

        // Assert
        Assert.Equal("System", dto.ModifiedBy);
    }

    [Fact]
    public void BookingUpdateDto_ShouldHandleConfirmedStatus()
    {
        // Arrange
        var dto = new BookingUpdateDto();

        // Act
        dto.Status = "Confirmed";

        // Assert
        Assert.Equal("Confirmed", dto.Status);
    }

    [Fact]
    public void BookingUpdateDto_ShouldHandleCancelledStatus()
    {
        // Arrange
        var dto = new BookingUpdateDto();

        // Act
        dto.Status = "Cancelled";

        // Assert
        Assert.Equal("Cancelled", dto.Status);
    }

    [Fact]
    public void BookingUpdateDto_ShouldHandleCompletedStatus()
    {
        // Arrange
        var dto = new BookingUpdateDto();

        // Act
        dto.Status = "Completed";

        // Assert
        Assert.Equal("Completed", dto.Status);
    }

    [Fact]
    public void BookingUpdateDto_ShouldHandleZeroNumberOfPeople()
    {
        // Arrange
        var dto = new BookingUpdateDto();

        // Act
        dto.NumberOfPeople = 0;

        // Assert
        Assert.Equal(0, dto.NumberOfPeople);
    }

    [Fact]
    public void BookingUpdateDto_ShouldHandleZeroTotalPrice()
    {
        // Arrange
        var dto = new BookingUpdateDto();

        // Act
        dto.TotalPrice = 0m;

        // Assert
        Assert.Equal(0m, dto.TotalPrice);
    }
}
