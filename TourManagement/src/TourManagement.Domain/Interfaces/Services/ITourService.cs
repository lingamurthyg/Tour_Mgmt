using TourManagement.Domain.Entities;

namespace TourManagement.Domain.Interfaces.Services;

/// <summary>
/// Service interface for Tour business logic
/// </summary>
public interface ITourService
{
    Task<IEnumerable<object>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<object?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<object> CreateAsync(object createDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, object updateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<object>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
