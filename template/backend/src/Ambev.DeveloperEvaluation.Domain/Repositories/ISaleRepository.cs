using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Sale entity.
    /// </summary>
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default);

        Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        Task ReplaceAsync(Sale sale, CancellationToken cancellationToken = default);

        Task<Sale?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
