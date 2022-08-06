using Adesso.Domain.Models;
using Adesso.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adesso.Infrastructure.Persistence.EntityConfigurations;

public class OrderItemEntityConfiguration : BaseEntityConfiguration<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);

        builder.ToTable("OrderItems", AdessoDbContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.Product)
            .WithMany(i => i.OrderItems)
            .HasForeignKey(i => i.ProductId);

    }
}


