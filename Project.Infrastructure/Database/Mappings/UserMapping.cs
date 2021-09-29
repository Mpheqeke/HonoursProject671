using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class UserEntityConfiguration : EntityCongurationMapper<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(x => x.CompanyRepresentatives).WithOne(c => c.User).HasPrincipalKey(c => c.Id);
            //builder.HasMany(x => x.UserPermissions).WithOne(c => c.User).HasPrincipalKey(c => c.Id);
            builder.HasMany(x => x.UserDocuments).WithOne(c => c.User).HasPrincipalKey(c => c.Id);
            builder.HasMany(x => x.UserSkillGains).WithOne(c => c.User).HasPrincipalKey(c => c.Id).HasForeignKey(u => u.UserId);
            
            builder.HasOne(x => x.Role).WithMany(c => c.Users).HasPrincipalKey(c => c.Id); //Added in new
            builder.ToTable("User");
        }
    }
}
