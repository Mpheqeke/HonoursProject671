using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class StatusEntityConfiguration : EntityCongurationMapper<Status>
    {
        public override void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(y => y.UserDocuments).WithOne(c => c.Status).HasPrincipalKey(c => c.Id);
            builder.HasMany(x => x.UserJobApplications).WithOne(c => c.Status).HasPrincipalKey(c => c.Id);
            builder.ToTable("Status");
        }
    }
}
