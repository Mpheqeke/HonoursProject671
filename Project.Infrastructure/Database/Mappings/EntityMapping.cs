using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Infrastructure.Database
{
	public abstract class EntityCongurationMapper<T> : IEntityTypeConfiguration<T> where T : class
	{
		public abstract void Configure(EntityTypeBuilder<T> builder);
	}
}
