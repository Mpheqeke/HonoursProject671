﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTOs;
using Project.Core.Interfaces;
using Project.Core.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        #region Company Select and Search Related Queries
        //Retreive all Companies
        [Route("~/api/Company/GetCompanies")]
        [HttpGet]
        public ActionResult<List<Company>> GetCompanies()
        {
            var users = _companyService.GetCompanies();
            return users;
        }

        //Retreive company by Id
        [Route("~/api/Company/GetSpecificCompany/{CompId}")]
        [HttpGet("{CompId}")]
        public ActionResult<List<CompanyDTO>> GetSpecificCompany(int CompId)
        {
            var company = _companyService.GetSpecificCompany(CompId);
            return company;
        }

        //Retreive all Company Representatives
        [Route("~/api/Company/GetCompSpecificReps/{CompId}")]
        [HttpGet("{CompId}")]
        public ActionResult<List<CompanyRepsDTO>> GetCompSpecificReps(int CompId)
        {
            var CompanyReps = _companyService.GetCompSpecificReps(CompId);
            return CompanyReps;
        }
        #endregion

        #region Company CRUD Related Queries (Create, Update, Delete)
        //Create new company
        [Route("~/api/Company/CreateCompany")]
        [HttpPost]
        public void CreateCompany([FromBody] Company company)
        {
            _companyService.CreateCompany(company);
        }

        //Remove a company (FK CONSTRAINT ERROR)
                //The DELETE statement conflicted with the REFERENCE constraint "FK_CompanyRepresentative_Company". 
                //The conflict occurred in database "ITRI671Project", table "dbo.CompanyRepresentative", column 'CompanyId'.
                //The statement has been terminated.
        [Route("~/api/Company/DeleteCompany/{id}")]
        [HttpDelete("{id}")]
        public void DeleteCompany(int id)
        {
            _companyService.DeleteCompany(id);
        }

        //Update existing company
        [Route("~/api/Company/UpdateCompany/{id}")]
        [HttpPut("{id}")]
        public void UpdateCompany(int id, [FromBody] Company company)
        {
            _companyService.UpdateCompany(id, company);
        }
        #endregion

        #region Vacancy Select and Search Related Queries
        //Retreive all Vacancies
        [Route("~/api/Company/GetVacancies")]
        [HttpGet]
        public ActionResult<List<VacanciesDTO>> GetVacancies(int? pageNumber, string sort)
        {
            int curPage = pageNumber ?? 1;
            int curPageSize = 30;

            var vacancies = _companyService.GetVacancies(sort);
            return Ok(vacancies.Skip((curPage - 1) * curPageSize).Take(curPageSize));
        }

        //Search all Vacancies
        [Route("~/api/Company/SearchVacancies")]
        [HttpGet]
        public ActionResult<List<VacanciesDTO>> SearchVacancies(int? pageNumber, string sort, string search)
        {
            int curPage = pageNumber ?? 1;
            int curPageSize = 30;

            var vacancies = _companyService.SearchVacancies(sort, search);
            return Ok(vacancies.Skip((curPage - 1) * curPageSize).Take(curPageSize));
        }

        //Retreive vacancy by Id
        [Route("~/api/Vacancy/{id}")]
        [HttpGet("{id}")]
        public ActionResult<List<SpecificVacancyDetailsDTO>> GetVacancyInfo(int id)
        {
            var vacancy = _companyService.GetVacancyInfo(id);
            return vacancy;
        }

        //Retreive all vacancies by company id
        [Route("~/api/Vacancy/Company/{id}")]
        [HttpGet("{id}")]
        public ActionResult<List<CompanySpecificVacanciesDTO>> GetCompanyVacancies(int id)
        {
            var vacancy = _companyService.GetCompanyVacancies(id);
            return vacancy;
        }
        #endregion

        #region Vacancy CRUD Related Queries (Create, Update, Delete)
        //Create new position
        [Route("~/api/Company/Vacancy/CreatePosition/{CompId}")]
        [HttpPost]
        public void CreatePosition([FromBody] Vacancy vacancy, int CompId)
        {
            _companyService.CreatePosition(vacancy, CompId);
        }

        //Remove a position (FK CONSTRAINT ERROR)
                //The DELETE statement conflicted with the REFERENCE constraint "FK_UserJobApplication_Vacancy". 
                //The conflict occurred in database "ITRI671Project", table "dbo.UserJobApplication", column 'VacancyId'.
                //The statement has been terminated.
        [Route("~/api/Company/Vacancy/DeletePosition/{VacId}")]
        [HttpDelete("{VacId}")]
        public void DeletePosition(int VacId)
        {
            _companyService.DeletePosition(VacId);
        }

        //Update existing position
        [Route("~/api/Company/Vacancy/UpdatePosition/{CompId}/{VacId}")]
        [HttpPut("{VacId}")]
        public void UpdatePosition(int VacId, int CompId, [FromBody] Vacancy vacancy)
        {
            _companyService.UpdatePosition(VacId, CompId, vacancy);
        }
        #endregion

        #region User Application Related Queries 
        //View all applications for specific company position (WERK)
        [Route("~/api/Company/GetPositionApplicants/{VacId}")]
        [HttpGet("{VacId}")]
        public ActionResult<List<CompanyApplicantsDTO>> GetPositionApplicants(int VacId)
        {
            var applicants = _companyService.GetPositionApplicants(VacId);
            return applicants;
        }

        //View specific applicant profile (Sets Status = Viewed) (WERK)
        [Route("~/api/Company/Vacancy/ViewSpecificApplicantProfile/{compId}/{userId}")]
        [HttpGet("{userId}")]
        public ActionResult<List<UserDTO>> ViewSpecificApplicantProfile(int userId, int compId)
        {
            var applicantProfile = _companyService.ViewSpecificApplicantProfile(userId, compId);
            return applicantProfile;
        }

        //Approve an application (WERK)
        [Route("~/api/Company/Vacancy/ApproveApplication/{CompId}/{appliId}")]
        [HttpPut("{appliId}")]
        public void ApproveApplication(int compId, int appliId, [FromBody] UserJobApplication application)
        {
            _companyService.ApproveApplication(compId, appliId, application);
        }

        //Reject an application (WERK)
        [Route("~/api/Company/Vacancy/RejectApplication/{CompId}/{appliId}")]
        [HttpPut("{appliId}")]
        public void RejectApplication(int compId, int appliId, [FromBody] UserJobApplication application)
        {
            _companyService.RejectApplication(compId, appliId, application);
        }
        #endregion

    }
}
