using Adesso.Application.Interfaces.Repositories;
using Adesso.Domain.Models;
using Adesso.Infrastructure.Persistence.Contexts;

namespace Adesso.Infrastructure.Persistence.Repositories.EFCore;

public class EFMoneyPointRepository : EFGenericRepository<MoneyPoint>, IMoneyPointRepository
{
    public EFMoneyPointRepository(AdessoDbContext dbContext) : base(dbContext)
    {
    }
}

