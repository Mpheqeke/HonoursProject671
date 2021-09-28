using Project.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Infrastructure.Database;

namespace Project.Infrastructure.Database.Mappings
{
    public class UserJobApplicationEntityConfiguration : EntityCongurationMapper<UserJobApplication>
    {
        public override void Configure(EntityTypeBuilder<UserJobApplication> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(x => x.Vacancy).WithMany(c => c.UserJobApplication).HasPrincipalKey(c => c.Id);
            builder.HasOne(x => x.Skill).WithMany(c => c.UserJobApplications).HasPrincipalKey(c => c.Id);
            builder.HasOne(x => x.Status).WithMany(c => c.UserJobApplications).HasPrincipalKey(c => c.Id);
            builder.HasOne(x => x.User).WithMany(c => c.UserJobApplications).HasPrincipalKey(c => c.Id).HasForeignKey(x => x.UserId);
            builder.ToTable("UserJobApplication");
        }
    }
}
