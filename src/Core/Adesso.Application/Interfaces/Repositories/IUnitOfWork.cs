using Adesso.Domain.Models;

namespace Adesso.Application.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GetRepository<T>() where T : BaseEntity;
    int SaveChanges();
}
