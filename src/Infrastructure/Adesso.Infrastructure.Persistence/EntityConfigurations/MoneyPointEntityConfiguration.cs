using Adesso.Domain.Models;
using Adesso.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adesso.Infrastructure.Persistence.EntityConfigurations;

public class MoneyPointEntityConfiguration : BaseEntityConfiguration<MoneyPoint>
{
    public override void Configure(EntityTypeBuilder<MoneyPoint> builder)
    {
        base.Configure(builder);

        builder.ToTable("MoneyPoints", AdessoDbContext.DEFAULT_SCHEMA);

    }
}


