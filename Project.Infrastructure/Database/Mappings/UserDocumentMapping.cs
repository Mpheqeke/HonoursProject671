using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class UserDocumentEntityConfiguration : EntityCongurationMapper<UserDocument>
    {
        public override void Configure(EntityTypeBuilder<UserDocument> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(x => x.DocumentType).WithMany(c => c.UserDocuments).HasPrincipalKey(c => c.Id);
            builder.HasOne(x => x.Status).WithMany(c => c.UserDocuments).HasPrincipalKey(c => c.Id);
            builder.HasOne(x => x.User).WithMany(c => c.UserDocuments).HasPrincipalKey(c => c.Id);
            builder.HasMany(x => x.UserSkillGains).WithOne(c => c.UserDocument).HasPrincipalKey(c => c.Id).HasForeignKey(x => x.UserId);
            builder.ToTable("UserDocument");
        }
    }
}
