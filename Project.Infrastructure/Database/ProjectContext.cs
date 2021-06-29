using System;
using System.Linq;
using System.Reflection;
using Project.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Project.Infrastructure.Database
{
	public interface IProjectContext
	{
		DbSet<TEntity> Set<TEntity>() where TEntity : class;

		int SaveChanges();
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Company> Company { get; set; }
        DbSet<CompanyRepresentative> CompanyRepresentative { get; set; }
        DbSet<DocumentType> DocumentTypes { get; set; }
        DbSet<Moocs> Moocs { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<Role> Role { get; set; }
        DbSet<RolePermission> RolePermissions { get; set; }
        DbSet<Skill> Skills { get; set; }
        DbSet<Status> Statuses { get; set; }
        DbSet<UserDocument> UserDocuments { get; set; }
        DbSet<UserJobApplication> UserJobApplications { get; set; }
        DbSet<UserPermission> UserPermissions { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<Vacancy> Vacancies { get; set; }
		
     }

	public class ProjectContext : DbContext, IProjectContext
	{
		public ProjectContext(DbContextOptions<ProjectContext> options)
		: base(options)
		{ }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
				.Where(type => !String.IsNullOrEmpty(type.Namespace))
				.Where(type => type.BaseType != null && type.BaseType.IsGenericType)
				.Where(type => type.Namespace == "Project.Infrastructure.Database.Mappings");

			foreach (var type in typesToRegister)
			{
				dynamic configInstance = Activator.CreateInstance(type);
				modelBuilder.ApplyConfiguration(configInstance);
			}
		}

          public  DbSet<Role> Roles { get; set; }
          public  DbSet<User> Users { get; set; }
          public  DbSet<Company> Company { get; set; }
          public  DbSet<CompanyRepresentative> CompanyRepresentative { get; set; }
          public  DbSet<DocumentType> DocumentTypes { get; set; }
          public  DbSet<Moocs> Moocs { get; set; }
          public  DbSet<Permission> Permissions { get; set; }
          public  DbSet<Role> Role { get; set; }
          public  DbSet<RolePermission> RolePermissions { get; set; }
          public  DbSet<Skill> Skills { get; set; }
          public  DbSet<Status> Statuses { get; set; }
          public  DbSet<UserDocument> UserDocuments { get; set; }
          public  DbSet<UserJobApplication> UserJobApplications { get; set; }
          public  DbSet<UserPermission> UserPermissions { get; set; }
          public  DbSet<UserRole> UserRoles { get; set; }
          public  DbSet<Vacancy> Vacancies { get; set; }

    }

}
