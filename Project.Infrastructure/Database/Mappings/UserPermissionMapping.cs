using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class UserPermissionEntityConfiguration : EntityCongurationMapper<UserPermission>
    {
        public override void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(x => x.User).WithMany(c => c.UserPermissions).HasPrincipalKey(c => c.Id);
            builder.HasOne(y => y.Permission).WithMany(c => c.UserPermissions).HasPrincipalKey(c => c.Id);
            builder.HasOne(y => y.Permission).WithMany(c => c.UserPermissions).HasPrincipalKey(c => c.Id);
            builder.ToTable("UserPermission");
        }
    }
}
