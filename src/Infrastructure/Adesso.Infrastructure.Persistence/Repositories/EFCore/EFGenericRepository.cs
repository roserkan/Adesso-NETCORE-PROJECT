using Adesso.Application.Interfaces.Repositories;
using Adesso.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Adesso.Infrastructure.Persistence.Repositories.EFCore;

public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbContext dbContext;


    protected DbSet<TEntity> entity => dbContext.Set<TEntity>();

    public EFGenericRepository(DbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    #region Insert Methods

    public virtual async Task<int> AddAsync(TEntity entity)
    {
        await this.entity.AddAsync(entity);
        //await dbContext.SaveChangesAsync();
        return entity.Id;   
    }

    public virtual int Add(TEntity entity)
    {
        this.entity.Add(entity);
        //await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public virtual async Task<int> AddAsync(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return 0;

        await entity.AddRangeAsync(entities);
        //await dbContext.SaveChangesAsync();
        return 1;
    }

    public virtual int Add(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return 0;

        entity.AddRange(entity);
        //await dbContext.SaveChangesAsync();
        return 1;
    }

    #endregion

    #region Update Methods

    public virtual async Task<int> UpdateAsync(TEntity entity)
    {
        this.entity.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;

        //await dbContext.SaveChangesAsync();
        return await Task.Run(() => entity.Id); 
    }

    public virtual int Update(TEntity entity)
    {
        this.entity.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;

        return entity.Id;
    }

    #endregion

    #region Delete Methods

    public async virtual Task<int> DeleteAsync(TEntity entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached)
        {
            this.entity.Attach(entity);
        }

        this.entity.Remove(entity);

        return await Task.Run(() => entity.Id);
    }

    public async virtual Task<int> DeleteAsync(int id)
    {
        var entity = this.entity.Find(id);
        return await DeleteAsync(entity);
    }

    public virtual int Delete(int id)
    {
        var entity = this.entity.Find(id);
        return Delete(entity);
    }

    public virtual int Delete(TEntity entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached)
        {
            this.entity.Attach(entity);
        }

        this.entity.Remove(entity);

        return entity.Id;
    }

    public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
    {
        dbContext.RemoveRange(entity.Where(predicate));
        return true;
    }

    public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
    {
        dbContext.RemoveRange(entity.Where(predicate));
        return await Task.Run(() => true);
    }

    #endregion

    #region AddOrUpdate Methods

    public async virtual Task<int> AddOrUpdateAsync(TEntity entity)
    {
        // check the entity with the id already tracked
        if (!this.entity.Local.Any(i => EqualityComparer<int>.Default.Equals(i.Id, entity.Id)))
            dbContext.Update(entity);

        return await Task.Run(() => entity.Id);
    }

    public virtual int AddOrUpdate(TEntity entity)
    {
        if (!this.entity.Local.Any(i => EqualityComparer<int>.Default.Equals(i.Id, entity.Id)))
            dbContext.Update(entity);

        return entity.Id;
    }

    #endregion

    #region Get Methods

    public virtual IQueryable<TEntity> AsQueryable() => entity.AsQueryable();
    public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = entity.AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        query = ApplyIncludes(query, includes);

        if (noTracking)
            query = query.AsNoTracking();

        return query;
    }


    public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        return Get(predicate, noTracking, includes).FirstOrDefaultAsync();
    }

    public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = entity;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        foreach (Expression<Func<TEntity, object>> include in includes)
        {
            query = query.Include(include);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (noTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    public virtual async Task<List<TEntity>> GetAll(bool noTracking = true)
    {
        if (noTracking)
            return await entity.AsNoTracking().ToListAsync();

        return await entity.ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(int id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        TEntity found = await entity.FindAsync(id);

        if (found == null)
            return null;

        if (noTracking)
            dbContext.Entry(found).State = EntityState.Detached;

        foreach (Expression<Func<TEntity, object>> include in includes)
        {
            dbContext.Entry(found).Reference(include).Load();
        }

        return found;
    }

    public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = entity;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        query = ApplyIncludes(query, includes);

        if (noTracking)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync();

    }

    #endregion

    #region Bulk Methods

    public virtual Task BulkDeleteById(IEnumerable<int> ids)
    {
        if (ids != null && !ids.Any())
            return Task.CompletedTask;

        dbContext.RemoveRange(entity.Where(i => ids.Contains(i.Id)));
        return Task.Run(() => 1);
    }

    public virtual Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
    {
        dbContext.RemoveRange(entity.Where(predicate));
        return Task.Run(() => 1);
    }

    public virtual Task BulkDelete(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return Task.CompletedTask;

        entity.RemoveRange(entities);
        return Task.Run(() => 1);
    }

    public virtual Task BulkUpdate(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return Task.CompletedTask;

        foreach (var entityItem in entities)
        {
            entity.Update(entityItem);
        }

        return Task.Run(() => 1);
    }

    public virtual async Task BulkAdd(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            await Task.CompletedTask;

        await entity.AddRangeAsync(entities);
       
    }

    #endregion

    #region SaveChanges Methods

    public Task<int> SaveChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }

    public int SaveChanges()
    {
        return dbContext.SaveChanges();
    }

    #endregion


    private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
    {
        if (includes != null)
        {
            foreach (var includeItem in includes)
            {
                query = query.Include(includeItem);
            }
        }

        return query;
    }
}