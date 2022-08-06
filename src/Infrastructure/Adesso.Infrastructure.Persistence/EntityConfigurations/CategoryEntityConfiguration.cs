using Adesso.Domain.Models;
using Adesso.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adesso.Infrastructure.Persistence.EntityConfigurations;

public class CategoryEntityConfiguration : BaseEntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.ToTable("Categories", AdessoDbContext.DEFAULT_SCHEMA);

        builder
          .HasOne(c => c.MoneyPoint)
          .WithOne(c => c.Category)
          .HasForeignKey<MoneyPoint>(c => c.CategoryId);
    }
}


