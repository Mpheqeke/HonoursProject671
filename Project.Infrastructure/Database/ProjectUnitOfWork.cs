using System.Collections.Generic;
using Project.Core.Interfaces;
using Project.Core.Models;
using Project.Infrastructure.Database;

namespace EntityConfigurationBase
{
    public class ProjectUnitOfWork : IProjectUnitOfWork
    {
        private readonly IProjectContext _context;
        
        private IRepository<Company> _company;
        private IRepository<CompanyRepresentative> _companyRepresentative;
        private IRepository<DocumentType> _documentType;
        private IRepository<Moocs> _moocs;
        private IRepository<Permission> _permission;
        private IRepository<Role> _role;
        private IRepository<RolePermission> _rolePermission;
        private IRepository<Skill> _skill;
        private IRepository<Status> _status;
        private IRepository<User> _user;
        private IRepository<UserDocument> _userDocument;
        private IRepository<UserJobApplication> _userJobApplication;
        private IRepository<UserPermission> _userPermission;
        private IRepository<UserSkillGain> _userSkillGain;
        private IRepository<Vacancy> _vacancy;


        public ProjectUnitOfWork(IProjectContext context)
        {
            _context = context;
        }

        public IRepository<Company> Company => _company ?? (_company = new Repository<Company>(_context));
        public IRepository<CompanyRepresentative> CompanyRepresentative => _companyRepresentative ?? (_companyRepresentative = new Repository<CompanyRepresentative>(_context));
        public IRepository<DocumentType> DocumentType => _documentType ?? (_documentType = new Repository<DocumentType>(_context));
        public IRepository<Moocs> Moocs => _moocs ?? (_moocs = new Repository<Moocs>(_context));
        public IRepository<Permission> Permission => _permission ?? (_permission = new Repository<Permission>(_context));
        public IRepository<Role> Role => _role ?? (_role = new Repository<Role>(_context));
        public IRepository<RolePermission> RolePermission => _rolePermission ?? (_rolePermission = new Repository<RolePermission>(_context));
        public IRepository<Skill> Skill => _skill ?? (_skill = new Repository<Skill>(_context));
        public IRepository<Status> Status => _status ?? (_status = new Repository<Status>(_context));
        public IRepository<User> User => _user ?? (_user = new Repository<User>(_context));
        public IRepository<UserDocument> UserDocument => _userDocument ?? (_userDocument = new Repository<UserDocument>(_context));
        public IRepository<UserJobApplication> UserJobApplication => _userJobApplication ?? (_userJobApplication = new Repository<UserJobApplication>(_context));
        public IRepository<UserPermission> UserPermission => _userPermission ?? (_userPermission = new Repository<UserPermission>(_context));
        public IRepository<UserSkillGain> UserSkillGain => _userSkillGain ?? (_userSkillGain = new Repository<UserSkillGain>(_context));
        public IRepository<Vacancy> Vacancy => _vacancy ?? (_vacancy  = new Repository<Vacancy>(_context));

        public void Save()
        {
            _context.SaveChanges();
        }

        //public T QueryDatabaseStoredProcedure<T>(string query)
        //{
        //    return _context.Database.SqlQuery<T>(query).FirstOrDefault();
        //}

        //public void ExecuteDatabaseStoredProcedure(string query)
        //{
        //    _context.Database.ExecuteSqlCommand(query);
        //}

        //public List<T> QueryDatabaseStoredProcedureList<T>(string query)
        //{
        //    return _context.Database.SqlQuery<T>(query).ToList();
        //}

	}
}
