using Adesso.Application.Interfaces.Repositories;
using Adesso.Domain.Models;
using Adesso.Infrastructure.Persistence.Contexts;

namespace Adesso.Infrastructure.Persistence.Repositories.EFCore;

public class EFUserRepository : EFGenericRepository<User>, IUserRepository
{
    public EFUserRepository(AdessoDbContext dbContext) : base(dbContext)
    {
    }
}

