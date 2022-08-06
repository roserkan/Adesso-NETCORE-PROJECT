using Adesso.Application.Interfaces.Repositories;
using Adesso.Domain.Models;
using Adesso.Infrastructure.Persistence.Contexts;

namespace Adesso.Infrastructure.Persistence.Repositories.EFCore;

public class EFUserDetailRepository : EFGenericRepository<UserDetail>, IUserDetailRepository
{
    public EFUserDetailRepository(AdessoDbContext dbContext) : base(dbContext)
    {
    }
}

