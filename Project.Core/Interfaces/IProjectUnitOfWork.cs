using Project.Core.Models;

namespace Project.Core.Interfaces
{
    public interface IProjectUnitOfWork
	{
        IRepository<Company> Company { get; }
        IRepository<CompanyRepresentative> CompanyRepresentative { get; }
        IRepository<DocumentType> DocumentType { get; }
        IRepository<Moocs> Moocs { get; }
        IRepository<Permission> Permission { get; }
        IRepository<Role> Role { get; }
        IRepository<RolePermission> RolePermission { get;}
        IRepository<Skill> Skill { get; }
        IRepository<Status> Status { get; }
        IRepository<User> User { get; }
        IRepository<UserDocument> UserDocument { get; }
        IRepository<UserJobApplication> UserJobApplication { get; }
        IRepository<UserPermission> UserPermission { get; }
        IRepository<UserSkillGain> UserSkillGain { get; }
        IRepository<Vacancy> Vacancy { get; }
        void Save();
        //T QueryDatabaseStoredProcedure<T>(string query);
        //void ExecuteDatabaseStoredProcedure(string query);
        //List<T> QueryDatabaseStoredProcedureList<T>(string query);
    }
}
