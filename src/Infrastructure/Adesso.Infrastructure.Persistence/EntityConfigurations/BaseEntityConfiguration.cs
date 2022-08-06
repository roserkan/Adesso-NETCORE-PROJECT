using Adesso.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adesso.Infrastructure.Persistence.EntityConfigurations;

public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).ValueGeneratedOnAdd();
        builder.Property(i => i.CreatedDate).ValueGeneratedOnAdd();
    }
}


