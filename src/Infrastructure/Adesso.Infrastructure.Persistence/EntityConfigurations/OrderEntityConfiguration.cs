using Adesso.Domain.Models;
using Adesso.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adesso.Infrastructure.Persistence.EntityConfigurations;

public class OrderEntityConfiguration : BaseEntityConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.ToTable("Orders", AdessoDbContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.User)
            .WithMany(i => i.Orders)
            .HasForeignKey(i => i.UserId);


    }
}


