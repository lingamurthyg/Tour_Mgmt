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

public class BookingServiceTests
{
    private readonly Mock<IBookingRepository> _mockBookingRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<BookingService>> _mockLogger;
    private readonly BookingService _bookingService;

    public BookingServiceTests()
    {
        _mockBookingRepository = new Mock<IBookingRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<BookingService>>();
        _bookingService = new BookingService(_mockBookingRepository.Object, _mockMapper.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_ShouldInitializeService()
    {
        // Arrange & Act
        var service = new BookingService(_mockBookingRepository.Object, _mockMapper.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllBookings()
    {
        // Arrange
        var bookings = new List<Booking>
        {
            new Booking { Id = 1, UserId = 1, TourId = 1 },
            new Booking { Id = 2, UserId = 2, TourId = 2 }
        };
        var bookingDtos = new List<BookingDto>
        {
            new BookingDto { Id = 1, UserId = 1, TourId = 1 },
            new BookingDto { Id = 2, UserId = 2, TourId = 2 }
        };

        _mockBookingRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(bookings);
        _mockMapper.Setup(m => m.Map<IEnumerable<BookingDto>>(bookings))
            .Returns(bookingDtos);

        // Act
        var result = await _bookingService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        _mockBookingRepository.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyListWhenNoBookings()
    {
        // Arrange
        var bookings = new List<Booking>();
        var bookingDtos = new List<BookingDto>();

        _mockBookingRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(bookings);
        _mockMapper.Setup(m => m.Map<IEnumerable<BookingDto>>(bookings))
            .Returns(bookingDtos);

        // Act
        var result = await _bookingService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        _mockBookingRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _bookingService.GetAllAsync());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnBookingWhenExists()
    {
        // Arrange
        var bookingId = 123;
        var booking = new Booking { Id = bookingId, UserId = 1, TourId = 1 };
        var bookingDto = new BookingDto { Id = bookingId, UserId = 1, TourId = 1 };

        _mockBookingRepository.Setup(r => r.GetByIdAsync(bookingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(booking);
        _mockMapper.Setup(m => m.Map<BookingDto>(booking))
            .Returns(bookingDto);

        // Act
        var result = await _bookingService.GetByIdAsync(bookingId);

        // Assert
        Assert.NotNull(result);
        _mockBookingRepository.Verify(r => r.GetByIdAsync(bookingId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNullWhenBookingNotFound()
    {
        // Arrange
        var bookingId = 999;
        _mockBookingRepository.Setup(r => r.GetByIdAsync(bookingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Booking?)null);

        // Act
        var result = await _bookingService.GetByIdAsync(bookingId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var bookingId = 1;
        _mockBookingRepository.Setup(r => r.GetByIdAsync(bookingId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _bookingService.GetByIdAsync(bookingId));
    }

    [Fact]
    public async Task GetByUserIdAsync_ShouldReturnUserBookings()
    {
        // Arrange
        var userId = 5;
        var bookings = new List<Booking>
        {
            new Booking { Id = 1, UserId = userId, TourId = 10 },
            new Booking { Id = 2, UserId = userId, TourId = 20 }
        };
        var bookingDtos = new List<BookingDto>
        {
            new BookingDto { Id = 1, UserId = userId, TourId = 10 },
            new BookingDto { Id = 2, UserId = userId, TourId = 20 }
        };

        _mockBookingRepository.Setup(r => r.GetByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(bookings);
        _mockMapper.Setup(m => m.Map<IEnumerable<BookingDto>>(bookings))
            .Returns(bookingDtos);

        // Act
        var result = await _bookingService.GetByUserIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        _mockBookingRepository.Verify(r => r.GetByUserIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByUserIdAsync_ShouldReturnEmptyListWhenNoBookingsForUser()
    {
        // Arrange
        var userId = 10;
        var bookings = new List<Booking>();
        var bookingDtos = new List<BookingDto>();

        _mockBookingRepository.Setup(r => r.GetByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(bookings);
        _mockMapper.Setup(m => m.Map<IEnumerable<BookingDto>>(bookings))
            .Returns(bookingDtos);

        // Act
        var result = await _bookingService.GetByUserIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByUserIdAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var userId = 1;
        _mockBookingRepository.Setup(r => r.GetByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _bookingService.GetByUserIdAsync(userId));
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateAndReturnBooking()
    {
        // Arrange
        var createDto = new BookingCreateDto { UserId = 1, TourId = 2, NumberOfPeople = 3 };
        var booking = new Booking { UserId = 1, TourId = 2, NumberOfPeople = 3 };
        var createdBooking = new Booking { Id = 1, UserId = 1, TourId = 2, NumberOfPeople = 3 };
        var bookingDto = new BookingDto { Id = 1, UserId = 1, TourId = 2, NumberOfPeople = 3 };

        _mockMapper.Setup(m => m.Map<Booking>(createDto))
            .Returns(booking);
        _mockBookingRepository.Setup(r => r.AddAsync(booking, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdBooking);
        _mockMapper.Setup(m => m.Map<BookingDto>(createdBooking))
            .Returns(bookingDto);

        // Act
        var result = await _bookingService.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        _mockBookingRepository.Verify(r => r.AddAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var createDto = new BookingCreateDto { UserId = 1, TourId = 2 };
        var booking = new Booking { UserId = 1, TourId = 2 };

        _mockMapper.Setup(m => m.Map<Booking>(createDto))
            .Returns(booking);
        _mockBookingRepository.Setup(r => r.AddAsync(booking, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _bookingService.CreateAsync(createDto));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateBookingWhenExists()
    {
        // Arrange
        var bookingId = 10;
        var updateDto = new BookingUpdateDto { NumberOfPeople = 5, Status = "Confirmed" };
        var existingBooking = new Booking { Id = bookingId, NumberOfPeople = 2, Status = "Pending" };

        _mockBookingRepository.Setup(r => r.GetByIdAsync(bookingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingBooking);
        _mockMapper.Setup(m => m.Map(updateDto, existingBooking))
            .Returns(existingBooking);
        _mockBookingRepository.Setup(r => r.UpdateAsync(existingBooking, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _bookingService.UpdateAsync(bookingId, updateDto);

        // Assert
        _mockBookingRepository.Verify(r => r.GetByIdAsync(bookingId, It.IsAny<CancellationToken>()), Times.Once);
        _mockBookingRepository.Verify(r => r.UpdateAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowEntityNotFoundExceptionWhenBookingNotExists()
    {
        // Arrange
        var bookingId = 999;
        var updateDto = new BookingUpdateDto { Status = "Confirmed" };

        _mockBookingRepository.Setup(r => r.GetByIdAsync(bookingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Booking?)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _bookingService.UpdateAsync(bookingId, updateDto));
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var bookingId = 10;
        var updateDto = new BookingUpdateDto { Status = "Confirmed" };
        var existingBooking = new Booking { Id = bookingId, Status = "Pending" };

        _mockBookingRepository.Setup(r => r.GetByIdAsync(bookingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingBooking);
        _mockMapper.Setup(m => m.Map(updateDto, existingBooking))
            .Returns(existingBooking);
        _mockBookingRepository.Setup(r => r.UpdateAsync(existingBooking, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _bookingService.UpdateAsync(bookingId, updateDto));
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteBookingWhenExists()
    {
        // Arrange
        var bookingId = 5;
        _mockBookingRepository.Setup(r => r.ExistsAsync(bookingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _mockBookingRepository.Setup(r => r.DeleteAsync(bookingId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _bookingService.DeleteAsync(bookingId);

        // Assert
        _mockBookingRepository.Verify(r => r.ExistsAsync(bookingId, It.IsAny<CancellationToken>()), Times.Once);
        _mockBookingRepository.Verify(r => r.DeleteAsync(bookingId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowEntityNotFoundExceptionWhenBookingNotExists()
    {
        // Arrange
        var bookingId = 999;
        _mockBookingRepository.Setup(r => r.ExistsAsync(bookingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _bookingService.DeleteAsync(bookingId));
        _mockBookingRepository.Verify(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var bookingId = 5;
        _mockBookingRepository.Setup(r => r.ExistsAsync(bookingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _mockBookingRepository.Setup(r => r.DeleteAsync(bookingId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _bookingService.DeleteAsync(bookingId));
    }
}
