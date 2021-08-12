using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class VacancyEntityConfiguration : EntityCongurationMapper<Vacancy>
    {
        public override void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.UserJobApplication).WithOne(y => y.Vacancy).HasPrincipalKey(y => y.Id);
            builder.HasMany(c => c.Skills).WithOne(y => y.Vacancy).HasPrincipalKey(y => y.Id);
            builder.ToTable("Vacancy");
        }
    }
}
