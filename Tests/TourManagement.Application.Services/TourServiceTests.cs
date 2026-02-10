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

public class TourServiceTests
{
    private readonly Mock<ITourRepository> _mockTourRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<TourService>> _mockLogger;
    private readonly TourService _tourService;

    public TourServiceTests()
    {
        _mockTourRepository = new Mock<ITourRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<TourService>>();
        _tourService = new TourService(_mockTourRepository.Object, _mockMapper.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_ShouldInitializeService()
    {
        // Arrange & Act
        var service = new TourService(_mockTourRepository.Object, _mockMapper.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTours()
    {
        // Arrange
        var tours = new List<Tour>
        {
            new Tour { Id = 1, TourName = "Tour 1" },
            new Tour { Id = 2, TourName = "Tour 2" }
        };
        var tourDtos = new List<TourDto>
        {
            new TourDto { Id = 1, TourName = "Tour 1" },
            new TourDto { Id = 2, TourName = "Tour 2" }
        };

        _mockTourRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(tours);
        _mockMapper.Setup(m => m.Map<IEnumerable<TourDto>>(tours))
            .Returns(tourDtos);

        // Act
        var result = await _tourService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        _mockTourRepository.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyListWhenNoTours()
    {
        // Arrange
        var tours = new List<Tour>();
        var tourDtos = new List<TourDto>();

        _mockTourRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(tours);
        _mockMapper.Setup(m => m.Map<IEnumerable<TourDto>>(tours))
            .Returns(tourDtos);

        // Act
        var result = await _tourService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        _mockTourRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _tourService.GetAllAsync());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTourWhenExists()
    {
        // Arrange
        var tourId = 123;
        var tour = new Tour { Id = tourId, TourName = "Test Tour" };
        var tourDto = new TourDto { Id = tourId, TourName = "Test Tour" };

        _mockTourRepository.Setup(r => r.GetByIdAsync(tourId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tour);
        _mockMapper.Setup(m => m.Map<TourDto>(tour))
            .Returns(tourDto);

        // Act
        var result = await _tourService.GetByIdAsync(tourId);

        // Assert
        Assert.NotNull(result);
        _mockTourRepository.Verify(r => r.GetByIdAsync(tourId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNullWhenTourNotFound()
    {
        // Arrange
        var tourId = 999;
        _mockTourRepository.Setup(r => r.GetByIdAsync(tourId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Tour?)null);

        // Act
        var result = await _tourService.GetByIdAsync(tourId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var tourId = 1;
        _mockTourRepository.Setup(r => r.GetByIdAsync(tourId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _tourService.GetByIdAsync(tourId));
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateAndReturnTour()
    {
        // Arrange
        var createDto = new TourCreateDto { TourName = "New Tour", Place = "Europe" };
        var tour = new Tour { TourName = "New Tour", Place = "Europe" };
        var createdTour = new Tour { Id = 1, TourName = "New Tour", Place = "Europe" };
        var tourDto = new TourDto { Id = 1, TourName = "New Tour", Place = "Europe" };

        _mockMapper.Setup(m => m.Map<Tour>(createDto))
            .Returns(tour);
        _mockTourRepository.Setup(r => r.AddAsync(tour, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdTour);
        _mockMapper.Setup(m => m.Map<TourDto>(createdTour))
            .Returns(tourDto);

        // Act
        var result = await _tourService.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        _mockTourRepository.Verify(r => r.AddAsync(It.IsAny<Tour>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var createDto = new TourCreateDto { TourName = "New Tour" };
        var tour = new Tour { TourName = "New Tour" };

        _mockMapper.Setup(m => m.Map<Tour>(createDto))
            .Returns(tour);
        _mockTourRepository.Setup(r => r.AddAsync(tour, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _tourService.CreateAsync(createDto));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateTourWhenExists()
    {
        // Arrange
        var tourId = 10;
        var updateDto = new TourUpdateDto { TourName = "Updated Tour" };
        var existingTour = new Tour { Id = tourId, TourName = "Old Tour" };

        _mockTourRepository.Setup(r => r.GetByIdAsync(tourId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingTour);
        _mockMapper.Setup(m => m.Map(updateDto, existingTour))
            .Returns(existingTour);
        _mockTourRepository.Setup(r => r.UpdateAsync(existingTour, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _tourService.UpdateAsync(tourId, updateDto);

        // Assert
        _mockTourRepository.Verify(r => r.GetByIdAsync(tourId, It.IsAny<CancellationToken>()), Times.Once);
        _mockTourRepository.Verify(r => r.UpdateAsync(It.IsAny<Tour>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowEntityNotFoundExceptionWhenTourNotExists()
    {
        // Arrange
        var tourId = 999;
        var updateDto = new TourUpdateDto { TourName = "Updated Tour" };

        _mockTourRepository.Setup(r => r.GetByIdAsync(tourId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Tour?)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _tourService.UpdateAsync(tourId, updateDto));
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var tourId = 10;
        var updateDto = new TourUpdateDto { TourName = "Updated Tour" };
        var existingTour = new Tour { Id = tourId, TourName = "Old Tour" };

        _mockTourRepository.Setup(r => r.GetByIdAsync(tourId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingTour);
        _mockMapper.Setup(m => m.Map(updateDto, existingTour))
            .Returns(existingTour);
        _mockTourRepository.Setup(r => r.UpdateAsync(existingTour, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _tourService.UpdateAsync(tourId, updateDto));
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteTourWhenExists()
    {
        // Arrange
        var tourId = 5;
        _mockTourRepository.Setup(r => r.ExistsAsync(tourId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _mockTourRepository.Setup(r => r.DeleteAsync(tourId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _tourService.DeleteAsync(tourId);

        // Assert
        _mockTourRepository.Verify(r => r.ExistsAsync(tourId, It.IsAny<CancellationToken>()), Times.Once);
        _mockTourRepository.Verify(r => r.DeleteAsync(tourId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowEntityNotFoundExceptionWhenTourNotExists()
    {
        // Arrange
        var tourId = 999;
        _mockTourRepository.Setup(r => r.ExistsAsync(tourId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _tourService.DeleteAsync(tourId));
        _mockTourRepository.Verify(r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var tourId = 5;
        _mockTourRepository.Setup(r => r.ExistsAsync(tourId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _mockTourRepository.Setup(r => r.DeleteAsync(tourId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _tourService.DeleteAsync(tourId));
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnMatchingTours()
    {
        // Arrange
        var searchTerm = "Europe";
        var tours = new List<Tour>
        {
            new Tour { Id = 1, TourName = "Europe Adventure" },
            new Tour { Id = 2, TourName = "European Tour" }
        };
        var tourDtos = new List<TourDto>
        {
            new TourDto { Id = 1, TourName = "Europe Adventure" },
            new TourDto { Id = 2, TourName = "European Tour" }
        };

        _mockTourRepository.Setup(r => r.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tours);
        _mockMapper.Setup(m => m.Map<IEnumerable<TourDto>>(tours))
            .Returns(tourDtos);

        // Act
        var result = await _tourService.SearchAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnEmptyListWhenNoMatches()
    {
        // Arrange
        var searchTerm = "NonExistent";
        var tours = new List<Tour>();
        var tourDtos = new List<TourDto>();

        _mockTourRepository.Setup(r => r.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tours);
        _mockMapper.Setup(m => m.Map<IEnumerable<TourDto>>(tours))
            .Returns(tourDtos);

        // Act
        var result = await _tourService.SearchAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task SearchAsync_ShouldThrowExceptionOnRepositoryError()
    {
        // Arrange
        var searchTerm = "Test";
        _mockTourRepository.Setup(r => r.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _tourService.SearchAsync(searchTerm));
    }
}
