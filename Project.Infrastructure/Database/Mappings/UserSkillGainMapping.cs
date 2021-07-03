using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class UserSkillGainEntityConfiguration : EntityCongurationMapper<UserSkillGain>
    {
        public override void Configure(EntityTypeBuilder<UserSkillGain> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(y => y.User).WithMany(c => c.UserSkillGains).HasPrincipalKey(c => c.Id);
            builder.HasOne(x => x.Moocs).WithMany(c => c.UserSkillGains).HasPrincipalKey(c => c.Id);
            builder.HasOne(v => v.Skill).WithMany(c => c.UserSkillGains).HasPrincipalKey(c => c.Id);
            builder.HasOne(d => d.UserDocument).WithMany(c => c.UserSkillGains).HasPrincipalKey(c => c.Id);
            builder.ToTable("UserSkillGain");
        }
    }
}
