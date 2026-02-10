namespace TourManagement.Domain.Interfaces.Services;

/// <summary>
/// Service interface for User business logic
/// </summary>
public interface IUserService
{
    Task<IEnumerable<object>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<object?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<object> CreateAsync(object createDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, object updateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<object?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
}
