using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class RoleEntityConfiguration : EntityCongurationMapper<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(x => x.RolePermissions).WithOne(c => c.Role).HasPrincipalKey(c => c.Id);
            builder.ToTable("Role");
        }
    }
}
