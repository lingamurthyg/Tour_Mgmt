using Xunit;
using System;
using TourManagement.Domain.Exceptions;

namespace TourManagement.Domain.Exceptions.Tests;

public class DomainExceptionTests
{
    [Fact]
    public void DomainException_ShouldCreateWithMessage()
    {
        // Arrange
        var message = "Domain error occurred";

        // Act
        var exception = new DomainException(message);

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(message, exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void DomainException_ShouldCreateWithMessageAndInnerException()
    {
        // Arrange
        var message = "Domain error with inner exception";
        var innerException = new InvalidOperationException("Inner error");

        // Act
        var exception = new DomainException(message, innerException);

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(message, exception.Message);
        Assert.NotNull(exception.InnerException);
        Assert.Equal(innerException, exception.InnerException);
    }

    [Fact]
    public void DomainException_ShouldBeInstanceOfException()
    {
        // Arrange & Act
        var exception = new DomainException("Test");

        // Assert
        Assert.IsAssignableFrom<Exception>(exception);
    }

    [Fact]
    public void DomainException_ShouldHandleEmptyMessage()
    {
        // Arrange & Act
        var exception = new DomainException(string.Empty);

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(string.Empty, exception.Message);
    }
}

public class EntityNotFoundExceptionTests
{
    [Fact]
    public void EntityNotFoundException_ShouldCreateWithEntityNameAndId()
    {
        // Arrange
        var entityName = "Tour";
        var id = 123;

        // Act
        var exception = new EntityNotFoundException(entityName, id);

        // Assert
        Assert.NotNull(exception);
        Assert.Contains(entityName, exception.Message);
        Assert.Contains(id.ToString(), exception.Message);
        Assert.Equal($"{entityName} with ID {id} was not found.", exception.Message);
    }

    [Fact]
    public void EntityNotFoundException_ShouldInheritFromDomainException()
    {
        // Arrange & Act
        var exception = new EntityNotFoundException("User", 1);

        // Assert
        Assert.IsAssignableFrom<DomainException>(exception);
        Assert.IsAssignableFrom<Exception>(exception);
    }

    [Fact]
    public void EntityNotFoundException_ShouldHandleDifferentEntityTypes()
    {
        // Arrange & Act
        var tourException = new EntityNotFoundException("Tour", 5);
        var userException = new EntityNotFoundException("User", 10);
        var bookingException = new EntityNotFoundException("Booking", 15);

        // Assert
        Assert.Contains("Tour with ID 5", tourException.Message);
        Assert.Contains("User with ID 10", userException.Message);
        Assert.Contains("Booking with ID 15", bookingException.Message);
    }

    [Fact]
    public void EntityNotFoundException_ShouldHandleZeroId()
    {
        // Arrange & Act
        var exception = new EntityNotFoundException("Entity", 0);

        // Assert
        Assert.Contains("Entity with ID 0 was not found.", exception.Message);
    }

    [Fact]
    public void EntityNotFoundException_ShouldHandleNegativeId()
    {
        // Arrange & Act
        var exception = new EntityNotFoundException("Entity", -1);

        // Assert
        Assert.Contains("Entity with ID -1 was not found.", exception.Message);
    }

    [Fact]
    public void EntityNotFoundException_ShouldHandleEmptyEntityName()
    {
        // Arrange & Act
        var exception = new EntityNotFoundException(string.Empty, 999);

        // Assert
        Assert.Contains("with ID 999 was not found.", exception.Message);
    }
}

public class BusinessRuleValidationExceptionTests
{
    [Fact]
    public void BusinessRuleValidationException_ShouldCreateWithMessage()
    {
        // Arrange
        var message = "Business rule validation failed";

        // Act
        var exception = new BusinessRuleValidationException(message);

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public void BusinessRuleValidationException_ShouldInheritFromDomainException()
    {
        // Arrange & Act
        var exception = new BusinessRuleValidationException("Validation error");

        // Assert
        Assert.IsAssignableFrom<DomainException>(exception);
        Assert.IsAssignableFrom<Exception>(exception);
    }

    [Fact]
    public void BusinessRuleValidationException_ShouldHandleEmptyMessage()
    {
        // Arrange & Act
        var exception = new BusinessRuleValidationException(string.Empty);

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(string.Empty, exception.Message);
    }

    [Fact]
    public void BusinessRuleValidationException_ShouldHandleComplexMessage()
    {
        // Arrange
        var message = "Cannot book tour: Number of people exceeds maximum capacity of 10";

        // Act
        var exception = new BusinessRuleValidationException(message);

        // Assert
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public void BusinessRuleValidationException_ShouldBeThrowable()
    {
        // Arrange
        var message = "Invalid operation";

        // Act
        void ThrowException() => throw new BusinessRuleValidationException(message);

        // Assert
        var exception = Assert.Throws<BusinessRuleValidationException>(ThrowException);
        Assert.Equal(message, exception.Message);
    }
}
