using Microsoft.EntityFrameworkCore.Storage;

namespace Challenge.EntityFramework.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        bool HasActiveTransaction { get; }
        IDbContextTransaction GetCurrentTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync(IDbContextTransaction transaction);
    }
}
