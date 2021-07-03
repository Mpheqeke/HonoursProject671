using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class CompanyRepresentativeEntityConfiguration : EntityCongurationMapper<CompanyRepresentative>
    {
        public override void Configure(EntityTypeBuilder<CompanyRepresentative> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.User).WithMany(x => x.CompanyRepresentatives).HasPrincipalKey(x => x.Id);
            builder.HasOne(y => y.Company).WithMany(e => e.CompanyRepresentatives).HasPrincipalKey(e => e.Id);
            builder.ToTable("CompanyRepresentative");
        }
    }
}
