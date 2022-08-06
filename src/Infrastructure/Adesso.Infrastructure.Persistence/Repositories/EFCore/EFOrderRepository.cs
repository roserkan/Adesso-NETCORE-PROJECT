using Adesso.Application.Interfaces.Repositories;
using Adesso.Domain.Models;
using Adesso.Infrastructure.Persistence.Contexts;

namespace Adesso.Infrastructure.Persistence.Repositories.EFCore;

public class EFOrderRepository : EFGenericRepository<Order>, IOrderRepository
{
    public EFOrderRepository(AdessoDbContext dbContext) : base(dbContext)
    {
    }
}

