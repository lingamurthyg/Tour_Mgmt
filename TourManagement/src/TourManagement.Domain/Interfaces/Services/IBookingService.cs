namespace TourManagement.Domain.Interfaces.Services;

/// <summary>
/// Service interface for Booking business logic
/// </summary>
public interface IBookingService
{
    Task<IEnumerable<object>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<object?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<object>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<object> CreateAsync(object createDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, object updateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
