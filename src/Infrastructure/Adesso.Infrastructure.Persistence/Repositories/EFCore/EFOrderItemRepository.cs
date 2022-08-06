using Adesso.Application.Interfaces.Repositories;
using Adesso.Domain.Models;
using Adesso.Infrastructure.Persistence.Contexts;

namespace Adesso.Infrastructure.Persistence.Repositories.EFCore;

public class EFOrderItemRepository : EFGenericRepository<OrderItem>, IOrderItemRepository
{
    public EFOrderItemRepository(AdessoDbContext dbContext) : base(dbContext)
    {
    }
}

