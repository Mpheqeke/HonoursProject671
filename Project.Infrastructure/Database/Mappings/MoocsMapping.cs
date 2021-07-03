using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class MoocsEntityConfiguration : EntityCongurationMapper<Moocs>
    {
        public override void Configure(EntityTypeBuilder<Moocs> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(y => y.UserSkillGains).WithOne(x => x.Moocs).HasPrincipalKey(x => x.Id);
            builder.ToTable("Moocs");
        }
    }
}
