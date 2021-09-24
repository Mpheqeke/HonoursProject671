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

        //Show All Available Vacancies
        public List<VacanciesDTO> GetVacancies(string sort)
        {
            var users = _unitOfWork.Company.Query(x => x.IsActive).Include("Vacancy").ToList().Select(x => x.DisplayVacanciesDTO).ToList();

            List<VacanciesDTO> usersWithVacancies = new List<VacanciesDTO>();

            //To only display companies with vacancies
            for (int i = 0; i <= users.Count() - 1; i++)
            {
                if (users[i].TotalVac > 0)
                {
                    usersWithVacancies.Add(users[i]);
                }
            }    

            //Sort by vacancy start date
            switch (sort)
            {
                case "desc":
                    return usersWithVacancies.OrderByDescending(o => o.StartDate).ToList();
                case "asc":
                    return usersWithVacancies.OrderBy(o => o.StartDate).ToList();
                default:
                    return usersWithVacancies;
            }
        }

        //Search for Available Vacancies
        public List<VacanciesDTO> SearchVacancies(string sort, string search)
        {
            var users = _unitOfWork.Company.Query(x => x.IsActive).Include("Vacancy").ToList().Select(x => x.DisplayVacanciesDTO).Where(s => s.Name.Contains(search) || s.Sector.Contains(search)).ToList();

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

        //Approve Company

        //Update Company Logo

        //Show Selected Company Information with Vacancies (With Pagination)

        //Create new Postion

        //Update Position Information

        //Remove Postion

        //Show Selected Vacancy Information
        public List<Vacancy> GetVacancyInfo(int id)
        {
            var vacancy = _unitOfWork.Vacancy.Query(x => x.Id == id).ToList();
            return vacancy;
        }

        //Show all Application for all Positions (With Pagination)

        //Show all Applications for Specific Position (With Pagination)

        //View Specific Applicant Profile (With Pagination)

        //View all Companies
        public List<Company> GetCompanies()
        {
            var users = _unitOfWork.Company.Query(x => x.IsActive).ToList();
            return users;
        }

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

    }
}
