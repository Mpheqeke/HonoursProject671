using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class SkillEntityConfiguration : EntityCongurationMapper<Skill>
    {
        public override void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(x => x.UserJobApplications).WithOne(c => c.Skill).HasPrincipalKey(c => c.Id);
            builder.HasMany(y => y.UserSkillGains).WithOne(c => c.Skill).HasPrincipalKey(c => c.Id);
            builder.ToTable("Skill");
        }
    }
}
