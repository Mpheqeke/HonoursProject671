using Project.Core.DTOs;
using Project.Core.Global;
using Project.Core.Interfaces;
using Project.Core.Models;
using Project.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.Core.AbstractFactories;
using System.Net.Http;
using System.Text;

namespace Project.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IProjectUnitOfWork _unitOfWork;

        public CompanyService(IProjectUnitOfWork unitOfWork/*, IAuthenticationHelper authentication, IAuthInfo authInfo*/)
        {
            //_authInfo = authInfo;
            _unitOfWork = unitOfWork;
            //_authentication = authentication;
        }

        //Approve Company

        //Update Company Logo

        #region Company Select and Search Related Queries
        //View all Companies (Entire Model instead of DTO)
        public List<Company> GetCompanies()
        {
            var users = _unitOfWork.Company.Query(x => x.IsActive).ToList();
            return users;
        }

        //View Specific Company Info
        public List<CompanyDTO> GetSpecificCompany(int CompId)
        {
            List<Company> comp = _unitOfWork.Company.Query(x => x.IsActive).ToList();

            var company = (from c in comp
                               where c.Id == CompId
                               select new CompanyDTO
                               {
                                   CompanyId = c.Id,
                                   CompanyName = c.Name,
                                   Mission = c.Mission,
                                   Vision = c.Vision,
                                   Sector = c.Sector,
                                   LogoUrl = c.LogoUrl
                               }).ToList();

            return company;
        }

        //View Company Specific Representatives
        public List<CompanyRepsDTO> GetCompSpecificReps(int CompId)
        {
            List<Company> comp = _unitOfWork.Company.Query(x => x.IsActive).ToList();
            List<CompanyRepresentative> comprep = _unitOfWork.CompanyRepresentative.Query().ToList();
            List<User> users = _unitOfWork.User.Query(x => x.IsActive).ToList();

            var reps = (from c in comp
                               join cr in comprep on CompId equals cr.CompanyId
                               join u in users on cr.UserId equals u.Id
                               where u.RoleId == 2 && c.Id == CompId
                               select new CompanyRepsDTO
                               {
                                   CompanyId = c.Id,
                                   RepId = cr.Id,
                                   RepFirstName = u.FirstName,
                                   RepLastName = u.LastName,
                                   RepProfileImageUrl = u.ImageUrl       
                               }).ToList();

            return reps;
        }
        #endregion

        #region Company CRUD Related Queries (Create, Update, Delete)
        //Create Company
        public void CreateCompany(Company company)
        {
            try
            {
                company.CreatedOn = DateTime.Now;
                company.ModifiedOn = DateTime.Now;
                company.CreatedBy = company.Name;
                company.ModifiedBy = company.Name;
                //Still need to be approved
                company.IsActive = false;

                _unitOfWork.Company.Add(company);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        //Remove Company (FK CONSTRAINT ERROR)
                //The DELETE statement conflicted with the REFERENCE constraint "FK_CompanyRepresentative_Company". 
                //The conflict occurred in database "ITRI671Project", table "dbo.CompanyRepresentative", column 'CompanyId'.
                //The statement has been terminated.
        public void DeleteCompany(int id)
        {
            try
            {
                var delObj = _unitOfWork.Company.Query(x => x.Id == id).SingleOrDefault();

                if (delObj != null)
                {
                    _unitOfWork.Company.Delete(delObj);
                    _unitOfWork.Save();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }

        //Update Company Information
        public void UpdateCompany(int id, Company company)
        {
            var updateObj = _unitOfWork.Company.Query(x => x.Id == id).SingleOrDefault();

            if (updateObj != null)
            {
                updateObj.Name = company.Name;
                updateObj.Sector = company.Sector;
                updateObj.Vision = company.Vision;
                updateObj.Mission = company.Mission;

                updateObj.ModifiedBy = company.Name;
                updateObj.ModifiedOn = DateTime.Now;

                _unitOfWork.Company.Update(updateObj);
                _unitOfWork.Save();
            }
        }
        #endregion

        #region Vacancy Select and Search Related Queries
        //Show All Available Vacancies (Vacancy Table)
        public List<VacanciesDTO> GetVacancies(string sort)
        {
            List<Company> comp = _unitOfWork.Company.Query(x => x.IsActive).ToList();
            List<Vacancy> vacan = _unitOfWork.Vacancy.Query(x => x.IsActive).ToList();

            var users = (from c in comp
                         join v in vacan on c.Id equals v.CompanyId
                         select new VacanciesDTO
                         {
                             CompanyId = c.Id,
                             CompanyName = c.Name,
                             Sector = c.Sector,
                             JobTitle = v.JobTitle,
                             StartDate = v.StartDate,
                             TotalVac = c.Vacancy.Count(x => x.CompanyId == c.Id)
                         }).ToList();

            //Sort by vacancy start date
            switch (sort)
            {
                case "desc":
                    return users.OrderByDescending(o => o.StartDate).ToList();
                case "asc":
                    return users.OrderBy(o => o.StartDate).ToList();
                default:
                    return users;
            }

        }

        //Search for Available Vacancies (Vacancy Table)
        public List<VacanciesDTO> SearchVacancies(string sort, string search)
        {
            List<Company> comp = _unitOfWork.Company.Query(x => x.IsActive).ToList();
            List<Vacancy> vacan = _unitOfWork.Vacancy.Query(x => x.IsActive).ToList();

            var users = (from c in comp
                         join v in vacan on c.Id equals v.CompanyId
                         where c.Name.ToUpper().Contains(search.ToUpper()) 
                            || c.Sector.ToUpper().Contains(search.ToUpper()) 
                            || v.JobTitle.ToUpper().Contains(search.ToUpper())
                         select new VacanciesDTO
                         {
                             CompanyId = c.Id,
                             CompanyName = c.Name,
                             Sector = c.Sector,
                             JobTitle = v.JobTitle,
                             StartDate = v.StartDate,
                             TotalVac = c.Vacancy.Count(x => x.CompanyId == c.Id)
                         }).ToList();

            //Sort by vacancy start date
            switch (sort)
            {
                case "desc":
                    return users.OrderByDescending(o => o.StartDate).ToList();
                case "asc":
                    return users.OrderBy(o => o.StartDate).ToList();
                default:
                    return users;
            }
        }

        //Get all vacancies for specific company
        public List<CompanySpecificVacanciesDTO> GetCompanyVacancies(int CompId)
        {
            List<Company> comp = _unitOfWork.Company.Query(x => x.IsActive).ToList();
            List<Vacancy> vacan = _unitOfWork.Vacancy.Query(x => x.IsActive).ToList();

            var vacancies = (from c in comp
                             join v in vacan on c.Id equals v.CompanyId
                             where v.CompanyId == CompId
                             select new CompanySpecificVacanciesDTO
                             {
                                 VacancyId = v.Id,
                                 JobTitle = v.JobTitle,
                                 ApplicationClosingDate = v.ApplicationClosingDate, //CHANGED FROM START DATE
                             }).ToList();


            //var vacancies = _unitOfWork.Vacancy.Query(a => a.IsActive).Where(a => a.CompanyId == id).ToList();
            return vacancies;
        } //Can also use for "View Submission" Page

        //Show all info of selected vacancy (NOGI REG)
        public List<SpecificVacancyDetailsDTO> GetVacancyInfo(int VacId)
        {
            List<Company> comp = _unitOfWork.Company.Query(x => x.IsActive).ToList();
            List<Vacancy> vacan = _unitOfWork.Vacancy.Query(x => x.IsActive).ToList();
            List<Skill> skills = _unitOfWork.Skill.Query().ToList();

            var vacancyInfo = (from c in comp
                               join v in vacan on c.Id equals v.CompanyId
                               join s in skills on v.SkillRequirementId equals s.Id
                               where v.Id == VacId
                               select new SpecificVacancyDetailsDTO
                               {
                                   JobTitle = v.JobTitle,
                                   CompanyId = c.Id,
                                   CompanyName = c.Name,
                                   Location = v.Location,
                                   JobDescription = v.JobDescription,
                                   Sector = c.Sector,
                                   StartDate = v.StartDate,
                                   Responsibilities = v.Responsibilities,
                                   SkillName = s.Name
                               }).ToList();

            return vacancyInfo;
        }
        #endregion

        #region Vacancy CRUD Related Queries (Create, Update, Delete)

        //Generate Position Reference Code
        public string GenerateRefCode()
        {
            //Add maybe check vir duplicates in DB
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            string RefCode = builder.ToString();

            return RefCode;
        }

        //Create new Postion
        public void CreatePosition(Vacancy vacancy, int CompId)
        {
            try
            {
                //Users add in VacancyName, JobTitle, StartDate, SkillRequirementsID, JobDescription, Location, Responsibilitites, and ApplicationClosingDate themselves

                var companyVac = _unitOfWork.Company.Query(x => x.Id == CompId).SingleOrDefault();

                vacancy.RefCode = GenerateRefCode();

                vacancy.CreatedOn = DateTime.Now;
                vacancy.ModifiedOn = DateTime.Now;
                vacancy.ApplictionOpeningDate = DateTime.Now;

                vacancy.CompanyId = CompId;
                vacancy.CreatedBy = companyVac.Name;
                vacancy.ModifiedBy = companyVac.Name;

                vacancy.IsActive = true;

                _unitOfWork.Vacancy.Add(vacancy);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        //Update Position Information
        public void UpdatePosition(int VacId, int CompId, Vacancy vacancy)
        {
            var updateObj = _unitOfWork.Vacancy.Query(x => x.Id == VacId).SingleOrDefault();
            var companyVac = _unitOfWork.Company.Query(x => x.Id == CompId).SingleOrDefault();

            if (updateObj != null)
            {
                updateObj.SkillRequirementId = vacancy.SkillRequirementId;
                updateObj.JobTitle = vacancy.JobTitle;
                updateObj.JobDescription = vacancy.JobDescription;
                updateObj.Location = vacancy.Location;
                updateObj.Responsibilities = vacancy.Responsibilities;
                updateObj.ApplicationClosingDate = vacancy.ApplicationClosingDate;
                updateObj.StartDate = vacancy.StartDate;

                updateObj.ModifiedBy = companyVac.Name;
                updateObj.ModifiedOn = DateTime.Now;

                _unitOfWork.Vacancy.Update(updateObj);
                _unitOfWork.Save();
            }
        }

        //Remove Postion (FK CONSTRAINT ERROR)
                //The DELETE statement conflicted with the REFERENCE constraint "FK_UserJobApplication_Vacancy". 
                //The conflict occurred in database "ITRI671Project", table "dbo.UserJobApplication", column 'VacancyId'.
                //The statement has been terminated.
        public void DeletePosition(int VacId)
        {
            try
            {
                var delObj = _unitOfWork.Vacancy.Query(x => x.Id == VacId).SingleOrDefault();

                if (delObj != null)
                {
                    _unitOfWork.Vacancy.Delete(delObj);
                    _unitOfWork.Save();
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }
        #endregion

        #region Job Application Related Queries
        //Show all Applications for all Positions (With Pagination) (Click UserId to view that User Profile)
        public List<CompanyApplicantsDTO> GetPositionApplicants(int vacId)
        {
            List<UserJobApplication> applications = _unitOfWork.UserJobApplication.Query(x => x.IsActive).ToList();
            List<Vacancy> vacan = _unitOfWork.Vacancy.Query(x => x.IsActive).ToList();
            List<User> user = _unitOfWork.User.Query(x => x.IsActive).ToList();

            var applicants = (from v in vacan
                              join a in applications on vacId equals a.VacancyId
                              join u in user on a.UserId equals u.Id //Not right format (Need to get UserId for this)(NOT IN DB YET)
                              where v.Id == vacId
                              select new CompanyApplicantsDTO
                              {
                                  UserId = u.Id,
                                  FirstName = u.FirstName,
                                  LastName = u.LastName,
                                  Motivation = a.Motivation,
                                  CVUrl = a.CVUrl
                              }).ToList();

            return applicants;
        }

        //Approve Application
        public void ApproveApplication(int compId, int appliId, UserJobApplication application)
        {
            var updateObj = _unitOfWork.UserJobApplication.Query(x => x.Id == appliId).SingleOrDefault();

            var companyVac = _unitOfWork.Company.Query(x => x.Id == compId).SingleOrDefault();

            if (updateObj != null)
            {
                updateObj.StatusId = 3;
                updateObj.ModifiedBy = companyVac.Name;
                updateObj.ModifiedOn = DateTime.Now;
                updateObj.IsActive = true;

                _unitOfWork.UserJobApplication.Update(updateObj);
                _unitOfWork.Save();
            }
        }

        //Reject Application
        public void RejectApplication(int compId, int appliId, UserJobApplication application)
        {
            var updateObj = _unitOfWork.UserJobApplication.Query(x => x.Id == appliId).SingleOrDefault();

            var companyVac = _unitOfWork.Company.Query(x => x.Id == compId).SingleOrDefault();

            if (updateObj != null)
            {
                updateObj.StatusId = 4;
                updateObj.ModifiedBy = companyVac.Name;
                updateObj.ModifiedOn = DateTime.Now;
                updateObj.IsActive = false;

                _unitOfWork.UserJobApplication.Update(updateObj);
                _unitOfWork.Save();
            }
        }

        //View Applicants Profile/Applicantion
        public List<UserDTO> ViewSpecificApplicantProfile(int userId, int compId)
        {
            List<User> user = _unitOfWork.User.Query(x => x.IsActive).ToList();
            var updateObj = _unitOfWork.UserJobApplication.Query(x => x.UserId == userId).SingleOrDefault();
            var companyVac = _unitOfWork.Company.Query(x => x.Id == compId).SingleOrDefault();

            if (updateObj != null)
            {
                updateObj.StatusId = 2;
                updateObj.ModifiedBy = companyVac.Name;
                updateObj.ModifiedOn = DateTime.Now;
                updateObj.IsActive = true;

                _unitOfWork.UserJobApplication.Update(updateObj);
                _unitOfWork.Save();
            }

            var applicantProfile = (from u in user
                                    where u.Id == userId
                                    select new UserDTO
                                    {
                                        UserId = userId,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                        Gender = u.Gender,
                                        Email = u.Email,
                                        Mobile = u.Mobile,
                                        ImageUrl = u.ImageUrl
                                    }).ToList();

            return applicantProfile;
        }
        #endregion

    }
}
