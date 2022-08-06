using Adesso.Application.Interfaces.Repositories;
using Adesso.Domain.Models;
using Adesso.Infrastructure.Persistence.Contexts;

namespace Adesso.Infrastructure.Persistence.Repositories.EFCore;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly AdessoDbContext _dbContext;

    public EFUnitOfWork(AdessoDbContext dbContext)
    {

        if (dbContext == null)
            throw new ArgumentNullException("dbContext can not be null.");

        _dbContext = dbContext;

        //_dbContext.Configuration.LazyLoadingEnabled = false;
        //_dbContext.Configuration.ValidateOnSaveEnabled = false;
        //_dbContext.Configuration.ProxyCreationEnabled = false;
    }

    #region IUnitOfWork Members
    public IGenericRepository<T> GetRepository<T>() where T : BaseEntity
    {
        return new EFGenericRepository<T>(_dbContext);
    }

    public int SaveChanges()
    {
        try
        {
            // Transaction işlemleri burada ele alınabilir veya Identity Map kurumsal tasarım kalıbı kullanılarak
            // sadece değişen alanları güncellemeyide sağlayabiliriz.
            return _dbContext.SaveChanges();
        }
        catch
        {
            // Burada DbEntityValidationException hatalarını handle edebiliriz.
            throw;
        }
    }
    #endregion

    #region IDisposable Members
    // Burada IUnitOfWork arayüzüne implemente ettiğimiz IDisposable arayüzünün Dispose Patternini implemente ediyoruz.
    private bool disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        this.disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion


}