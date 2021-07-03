using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class DocumentTypeEntityConfiguration : EntityCongurationMapper<DocumentType>
    {
        public override void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(x => x.UserDocuments).WithOne(c => c.DocumentType).HasPrincipalKey(c => c.Id);
            builder.ToTable("DocumentType");
        }
    }
}
