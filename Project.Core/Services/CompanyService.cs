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

        //Create Company

        //Update Company Information

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

        public List<Company> GetCompanies()
        {
            var users = _unitOfWork.Company.Query(x => x.IsActive).ToList();
            return users;
        }


    }
}
