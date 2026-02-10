using Xunit;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using TourManagement.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Moq;

namespace TourManagement.Web.ViewModels.Tests;

public class TourViewModelTests
{
    [Fact]
    public void TourViewModel_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var viewModel = new TourViewModel();

        // Assert
        Assert.NotNull(viewModel);
        Assert.Equal(0, viewModel.Id);
        Assert.Equal(string.Empty, viewModel.TourName);
        Assert.Equal(string.Empty, viewModel.Place);
        Assert.Equal(0, viewModel.Days);
        Assert.Equal(0m, viewModel.Price);
        Assert.Equal(string.Empty, viewModel.Locations);
        Assert.Equal(string.Empty, viewModel.TourInfo);
        Assert.Null(viewModel.PicturePath);
        Assert.Null(viewModel.PictureFile);
        Assert.Equal(default(DateTime), viewModel.CreatedDate);
    }

    [Fact]
    public void TourViewModel_ShouldSetAndGetAllProperties()
    {
        // Arrange
        var viewModel = new TourViewModel();
        var mockFile = new Mock<IFormFile>();
        var expectedId = 123;
        var expectedName = "Test Tour";
        var expectedPlace = "Europe";
        var expectedDays = 7;
        var expectedPrice = 1999.99m;
        var expectedLocations = "Paris, Rome";
        var expectedInfo = "Great tour";
        var expectedPath = "/images/tour.jpg";
        var expectedDate = DateTime.UtcNow;

        // Act
        viewModel.Id = expectedId;
        viewModel.TourName = expectedName;
        viewModel.Place = expectedPlace;
        viewModel.Days = expectedDays;
        viewModel.Price = expectedPrice;
        viewModel.Locations = expectedLocations;
        viewModel.TourInfo = expectedInfo;
        viewModel.PicturePath = expectedPath;
        viewModel.PictureFile = mockFile.Object;
        viewModel.CreatedDate = expectedDate;

        // Assert
        Assert.Equal(expectedId, viewModel.Id);
        Assert.Equal(expectedName, viewModel.TourName);
        Assert.Equal(expectedPlace, viewModel.Place);
        Assert.Equal(expectedDays, viewModel.Days);
        Assert.Equal(expectedPrice, viewModel.Price);
        Assert.Equal(expectedLocations, viewModel.Locations);
        Assert.Equal(expectedInfo, viewModel.TourInfo);
        Assert.Equal(expectedPath, viewModel.PicturePath);
        Assert.NotNull(viewModel.PictureFile);
        Assert.Equal(expectedDate, viewModel.CreatedDate);
    }

    [Fact]
    public void TourViewModel_TourName_ShouldBeRequired()
    {
        // Arrange
        var viewModel = new TourViewModel { TourName = "" };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourViewModel.TourName) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.TourName, context, results);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Tour name is required"));
    }

    [Fact]
    public void TourViewModel_Place_ShouldBeRequired()
    {
        // Arrange
        var viewModel = new TourViewModel { Place = "" };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourViewModel.Place) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.Place, context, results);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Place is required"));
    }

    [Fact]
    public void TourViewModel_Days_ShouldBeInRange()
    {
        // Arrange
        var viewModel = new TourViewModel { Days = 500 };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourViewModel.Days) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.Days, context, results);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Days must be between 1 and 365"));
    }

    [Fact]
    public void TourViewModel_Price_ShouldBeGreaterThanZero()
    {
        // Arrange
        var viewModel = new TourViewModel { Price = 0m };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourViewModel.Price) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.Price, context, results);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Price must be greater than 0"));
    }
}

public class TourCreateViewModelTests
{
    [Fact]
    public void TourCreateViewModel_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var viewModel = new TourCreateViewModel();

        // Assert
        Assert.NotNull(viewModel);
        Assert.Equal(string.Empty, viewModel.TourName);
        Assert.Equal(string.Empty, viewModel.Place);
        Assert.Equal(0, viewModel.Days);
        Assert.Equal(0m, viewModel.Price);
        Assert.Equal(string.Empty, viewModel.Locations);
        Assert.Equal(string.Empty, viewModel.TourInfo);
        Assert.Null(viewModel.PictureFile);
    }

    [Fact]
    public void TourCreateViewModel_ShouldSetAndGetAllProperties()
    {
        // Arrange
        var viewModel = new TourCreateViewModel();
        var mockFile = new Mock<IFormFile>();
        var expectedName = "New Tour";
        var expectedPlace = "Asia";
        var expectedDays = 14;
        var expectedPrice = 2999.99m;
        var expectedLocations = "Tokyo, Seoul";
        var expectedInfo = "Amazing tour";

        // Act
        viewModel.TourName = expectedName;
        viewModel.Place = expectedPlace;
        viewModel.Days = expectedDays;
        viewModel.Price = expectedPrice;
        viewModel.Locations = expectedLocations;
        viewModel.TourInfo = expectedInfo;
        viewModel.PictureFile = mockFile.Object;

        // Assert
        Assert.Equal(expectedName, viewModel.TourName);
        Assert.Equal(expectedPlace, viewModel.Place);
        Assert.Equal(expectedDays, viewModel.Days);
        Assert.Equal(expectedPrice, viewModel.Price);
        Assert.Equal(expectedLocations, viewModel.Locations);
        Assert.Equal(expectedInfo, viewModel.TourInfo);
        Assert.NotNull(viewModel.PictureFile);
    }

    [Fact]
    public void TourCreateViewModel_TourName_ShouldBeRequired()
    {
        // Arrange
        var viewModel = new TourCreateViewModel { TourName = "" };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourCreateViewModel.TourName) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.TourName, context, results);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Tour name is required"));
    }

    [Fact]
    public void TourCreateViewModel_TourName_ShouldEnforceMaxLength()
    {
        // Arrange
        var viewModel = new TourCreateViewModel { TourName = new string('A', 201) };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourCreateViewModel.TourName) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.TourName, context, results);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage.Contains("cannot exceed 200 characters"));
    }

    [Fact]
    public void TourCreateViewModel_Days_ShouldBeInValidRange()
    {
        // Arrange
        var viewModel = new TourCreateViewModel { Days = 0 };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourCreateViewModel.Days) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.Days, context, results);

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void TourCreateViewModel_Price_ShouldBeValid()
    {
        // Arrange
        var viewModel = new TourCreateViewModel { Price = 100m };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourCreateViewModel.Price) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.Price, context, results);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }
}

public class TourEditViewModelTests
{
    [Fact]
    public void TourEditViewModel_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var viewModel = new TourEditViewModel();

        // Assert
        Assert.NotNull(viewModel);
        Assert.Equal(0, viewModel.Id);
        Assert.Equal(string.Empty, viewModel.TourName);
        Assert.Equal(string.Empty, viewModel.Place);
        Assert.Equal(0, viewModel.Days);
        Assert.Equal(0m, viewModel.Price);
        Assert.Equal(string.Empty, viewModel.Locations);
        Assert.Equal(string.Empty, viewModel.TourInfo);
        Assert.Null(viewModel.ExistingPicturePath);
        Assert.Null(viewModel.PictureFile);
    }

    [Fact]
    public void TourEditViewModel_ShouldSetAndGetAllProperties()
    {
        // Arrange
        var viewModel = new TourEditViewModel();
        var mockFile = new Mock<IFormFile>();
        var expectedId = 456;
        var expectedName = "Updated Tour";
        var expectedPlace = "America";
        var expectedDays = 21;
        var expectedPrice = 3999.99m;
        var expectedLocations = "NYC, LA";
        var expectedInfo = "Updated info";
        var expectedPath = "/images/old.jpg";

        // Act
        viewModel.Id = expectedId;
        viewModel.TourName = expectedName;
        viewModel.Place = expectedPlace;
        viewModel.Days = expectedDays;
        viewModel.Price = expectedPrice;
        viewModel.Locations = expectedLocations;
        viewModel.TourInfo = expectedInfo;
        viewModel.ExistingPicturePath = expectedPath;
        viewModel.PictureFile = mockFile.Object;

        // Assert
        Assert.Equal(expectedId, viewModel.Id);
        Assert.Equal(expectedName, viewModel.TourName);
        Assert.Equal(expectedPlace, viewModel.Place);
        Assert.Equal(expectedDays, viewModel.Days);
        Assert.Equal(expectedPrice, viewModel.Price);
        Assert.Equal(expectedLocations, viewModel.Locations);
        Assert.Equal(expectedInfo, viewModel.TourInfo);
        Assert.Equal(expectedPath, viewModel.ExistingPicturePath);
        Assert.NotNull(viewModel.PictureFile);
    }

    [Fact]
    public void TourEditViewModel_TourName_ShouldBeRequired()
    {
        // Arrange
        var viewModel = new TourEditViewModel { TourName = "" };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourEditViewModel.TourName) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.TourName, context, results);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Tour name is required"));
    }

    [Fact]
    public void TourEditViewModel_Place_ShouldEnforceMaxLength()
    {
        // Arrange
        var viewModel = new TourEditViewModel { Place = new string('B', 201) };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourEditViewModel.Place) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.Place, context, results);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage.Contains("cannot exceed 200 characters"));
    }

    [Fact]
    public void TourEditViewModel_Locations_ShouldEnforceMaxLength()
    {
        // Arrange
        var viewModel = new TourEditViewModel { Locations = new string('C', 501) };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourEditViewModel.Locations) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.Locations, context, results);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage.Contains("cannot exceed 500 characters"));
    }

    [Fact]
    public void TourEditViewModel_TourInfo_ShouldEnforceMaxLength()
    {
        // Arrange
        var viewModel = new TourEditViewModel { TourInfo = new string('D', 2001) };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourEditViewModel.TourInfo) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.TourInfo, context, results);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage.Contains("cannot exceed 2000 characters"));
    }

    [Fact]
    public void TourEditViewModel_Days_ShouldAcceptValidValue()
    {
        // Arrange
        var viewModel = new TourEditViewModel { Days = 30 };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourEditViewModel.Days) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.Days, context, results);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void TourEditViewModel_Price_ShouldAcceptValidValue()
    {
        // Arrange
        var viewModel = new TourEditViewModel { Price = 500.50m };
        var context = new ValidationContext(viewModel) { MemberName = nameof(TourEditViewModel.Price) };
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateProperty(viewModel.Price, context, results);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }
}
