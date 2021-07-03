using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class UserRoleEntityConfiguration : EntityCongurationMapper<UserRole>
    {
        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(y => y.User).WithMany(c => c.UserRoles).HasPrincipalKey(c => c.Id);
            builder.HasOne(x => x.Role).WithMany(c => c.UserRoles).HasPrincipalKey(c => c.Id);
            builder.ToTable("UserRole");
        }
    }
}
