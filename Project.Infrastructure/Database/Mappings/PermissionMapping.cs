using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class PermissionEntityConfiguration : EntityCongurationMapper<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(x => x.UserPermissions).WithOne(d => d.Permission).HasPrincipalKey(d => d.Id);
            builder.ToTable("Permission");
        }
    }
}
