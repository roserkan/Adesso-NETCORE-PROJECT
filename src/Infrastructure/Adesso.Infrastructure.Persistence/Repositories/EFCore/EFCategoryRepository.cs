using Adesso.Application.Interfaces.Repositories;
using Adesso.Domain.Models;
using Adesso.Infrastructure.Persistence.Contexts;

namespace Adesso.Infrastructure.Persistence.Repositories.EFCore;

public class EFCategoryRepository : EFGenericRepository<Category>, ICategoryRepository
{
    public EFCategoryRepository(AdessoDbContext dbContext) : base(dbContext)
    {
    }
}

