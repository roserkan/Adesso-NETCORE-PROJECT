using Adesso.Domain.Models;
using Adesso.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adesso.Infrastructure.Persistence.EntityConfigurations;

public class UserDetailEntityConfiguration : BaseEntityConfiguration<UserDetail>
{
    public override void Configure(EntityTypeBuilder<UserDetail> builder)
    {
        base.Configure(builder);

        builder.ToTable("UserDetails", AdessoDbContext.DEFAULT_SCHEMA);

    }
}


