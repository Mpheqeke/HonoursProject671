using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class CompanyEntityConfiguration : EntityCongurationMapper<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.CompanyRepresentatives).WithOne(x => x.Company).HasPrincipalKey(x => x.Id);

            builder.ToTable("Company");
        }
    }
}
