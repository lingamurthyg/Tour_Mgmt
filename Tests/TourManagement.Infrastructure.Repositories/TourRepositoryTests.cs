using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourManagement.Infrastructure.Repositories;
using TourManagement.Infrastructure.Data;
using TourManagement.Domain.Entities;

namespace TourManagement.Infrastructure.Repositories.Tests;

public class TourRepositoryTests
{
    private readonly Mock<ILogger<TourRepository>> _mockLogger;
    private readonly DbContextOptions<TourManagementDbContext> _options;

    public TourRepositoryTests()
    {
        _mockLogger = new Mock<ILogger<TourRepository>>();
        _options = new DbContextOptionsBuilder<TourManagementDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Constructor_ShouldInitializeRepository()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);

        // Act
        var repository = new TourRepository(context, _mockLogger.Object);

        // Assert
        Assert.NotNull(repository);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnActiveTours()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        context.Tours.AddRange(
            new Tour { Id = 1, TourName = "Tour 1", IsActive = true, CreatedDate = DateTime.UtcNow },
            new Tour { Id = 2, TourName = "Tour 2", IsActive = true, CreatedDate = DateTime.UtcNow.AddDays(-1) },
            new Tour { Id = 3, TourName = "Tour 3", IsActive = false, CreatedDate = DateTime.UtcNow }
        );
        await context.SaveChangesAsync();

        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        var tourList = result.ToList();
        Assert.Equal(2, tourList.Count);
        Assert.All(tourList, tour => Assert.True(tour.IsActive));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyWhenNoActiveTours()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        context.Tours.Add(new Tour { Id = 1, TourName = "Inactive Tour", IsActive = false });
        await context.SaveChangesAsync();

        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTourWhenActiveAndExists()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        var tour = new Tour { Id = 1, TourName = "Test Tour", IsActive = true };
        context.Tours.Add(tour);
        await context.SaveChangesAsync();

        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Tour", result.TourName);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNullWhenTourNotActive()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        var tour = new Tour { Id = 1, TourName = "Inactive Tour", IsActive = false };
        context.Tours.Add(tour);
        await context.SaveChangesAsync();

        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNullWhenTourNotExists()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddTourAndReturnWithId()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        var repository = new TourRepository(context, _mockLogger.Object);
        var tour = new Tour { TourName = "New Tour", Place = "Europe", IsActive = true };

        // Act
        var result = await repository.AddAsync(tour);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Id);
        Assert.Equal("New Tour", result.TourName);
        Assert.Equal(1, await context.Tours.CountAsync());
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingTour()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        var tour = new Tour { TourName = "Original", IsActive = true };
        context.Tours.Add(tour);
        await context.SaveChangesAsync();

        var repository = new TourRepository(context, _mockLogger.Object);
        tour.TourName = "Updated";

        // Act
        await repository.UpdateAsync(tour);

        // Assert
        var updatedTour = await context.Tours.FindAsync(tour.Id);
        Assert.NotNull(updatedTour);
        Assert.Equal("Updated", updatedTour.TourName);
    }

    [Fact]
    public async Task DeleteAsync_ShouldSoftDeleteTour()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        var tour = new Tour { Id = 1, TourName = "To Delete", IsActive = true };
        context.Tours.Add(tour);
        await context.SaveChangesAsync();

        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        await repository.DeleteAsync(1);

        // Assert
        var deletedTour = await context.Tours.FindAsync(1);
        Assert.NotNull(deletedTour);
        Assert.False(deletedTour.IsActive);
        Assert.NotNull(deletedTour.ModifiedDate);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDoNothingWhenTourNotExists()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        await repository.DeleteAsync(999);

        // Assert - No exception thrown
        Assert.Equal(0, await context.Tours.CountAsync());
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrueWhenTourExistsAndActive()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        var tour = new Tour { Id = 1, TourName = "Exists", IsActive = true };
        context.Tours.Add(tour);
        await context.SaveChangesAsync();

        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.ExistsAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalseWhenTourNotActive()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        var tour = new Tour { Id = 1, TourName = "Inactive", IsActive = false };
        context.Tours.Add(tour);
        await context.SaveChangesAsync();

        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.ExistsAsync(1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalseWhenTourNotExists()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.ExistsAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnMatchingToursByName()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        context.Tours.AddRange(
            new Tour { TourName = "Europe Adventure", Place = "Europe", IsActive = true },
            new Tour { TourName = "Asia Tour", Place = "Asia", IsActive = true },
            new Tour { TourName = "European Vacation", Place = "Europe", IsActive = true }
        );
        await context.SaveChangesAsync();

        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.SearchAsync("Europe");

        // Assert
        Assert.NotNull(result);
        var tourList = result.ToList();
        Assert.Equal(2, tourList.Count);
        Assert.All(tourList, tour => Assert.Contains("Europe", tour.TourName));
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnMatchingToursByPlace()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        context.Tours.AddRange(
            new Tour { TourName = "Tour 1", Place = "Paris", IsActive = true },
            new Tour { TourName = "Tour 2", Place = "London", IsActive = true },
            new Tour { TourName = "Tour 3", Place = "Paris", IsActive = true }
        );
        await context.SaveChangesAsync();

        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.SearchAsync("Paris");

        // Assert
        Assert.NotNull(result);
        var tourList = result.ToList();
        Assert.Equal(2, tourList.Count);
        Assert.All(tourList, tour => Assert.Equal("Paris", tour.Place));
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnEmptyWhenNoMatches()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        context.Tours.Add(new Tour { TourName = "Test", Place = "Test", IsActive = true });
        await context.SaveChangesAsync();

        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.SearchAsync("NonExistent");

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByPlaceAsync_ShouldReturnToursForSpecificPlace()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        context.Tours.AddRange(
            new Tour { TourName = "Tour 1", Place = "Europe", IsActive = true },
            new Tour { TourName = "Tour 2", Place = "Asia", IsActive = true },
            new Tour { TourName = "Tour 3", Place = "Europe", IsActive = true }
        );
        await context.SaveChangesAsync();

        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.GetByPlaceAsync("Europe");

        // Assert
        Assert.NotNull(result);
        var tourList = result.ToList();
        Assert.Equal(2, tourList.Count);
        Assert.All(tourList, tour => Assert.Equal("Europe", tour.Place));
    }

    [Fact]
    public async Task GetByPlaceAsync_ShouldReturnEmptyWhenNoMatchingPlace()
    {
        // Arrange
        using var context = new TourManagementDbContext(_options);
        context.Tours.Add(new Tour { TourName = "Test", Place = "Europe", IsActive = true });
        await context.SaveChangesAsync();

        var repository = new TourRepository(context, _mockLogger.Object);

        // Act
        var result = await repository.GetByPlaceAsync("Asia");

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
