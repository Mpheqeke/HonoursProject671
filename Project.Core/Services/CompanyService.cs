﻿using Project.Core.DTOs;
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

        //Create new Postion

        //Update Position Information

        //Remove Postion

        //Show all Applications for all Positions (With Pagination)

        //Show all Applications for Specific Position (With Pagination)

        //View Specific Applicant Profile (With Pagination)

        //Approve Company

        //Update Company Logo

        #region Company Select and Search Related Queries
        //View all Companies
        public List<Company> GetCompanies()
        {
            var users = _unitOfWork.Company.Query(x => x.IsActive).ToList();
            return users;
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

        //Remove Company
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
        //Show All Available Vacancies
        public List<VacanciesDTO> GetVacancies(string sort)
        {
            List<Company> comp = _unitOfWork.Company.Query(x => x.IsActive).ToList();
            List<Vacancy> vacan = _unitOfWork.Vacancy.Query(x => x.IsActive).ToList();

            var users = (from c in comp
                         join v in vacan on c.Id equals v.CompanyId
                         select new VacanciesDTO
                         {
                             CompanyId = c.Id,
                             VacancyName = c.Name,
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

        //Search for Available Vacancies
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
                             VacancyName = c.Name,
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
                                 StartDate = v.StartDate,
                             }).ToList();


            //var vacancies = _unitOfWork.Vacancy.Query(a => a.IsActive).Where(a => a.CompanyId == id).ToList();
            return vacancies;
        }

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

        #region Vacancy CRUD Related Queries
        #endregion
    }
}
