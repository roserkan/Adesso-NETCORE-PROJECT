using Adesso.Domain.Models;
using Adesso.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adesso.Infrastructure.Persistence.EntityConfigurations;

public class UserRoleEntityConfiguration : BaseEntityConfiguration<UserRole>
{
    public override void Configure(EntityTypeBuilder<UserRole> builder)
    {
        base.Configure(builder);

        builder.ToTable("UserRoles", AdessoDbContext.DEFAULT_SCHEMA);


        builder 
          .HasOne(c => c.Role)
          .WithMany(c => c.UserRole)
          .HasForeignKey(c => c.RoleId);

        builder
          .HasOne(c => c.User)
          .WithMany(c => c.UserRole)
          .HasForeignKey(c => c.UserId);
    }
}


