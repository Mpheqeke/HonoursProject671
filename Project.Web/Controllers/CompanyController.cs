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
        public ActionResult<List<Vacancy>> GetVacancyInfo(int id)
        {
            var vacancy = _companyService.GetVacancyInfo(id);
            return vacancy;
        }

        //Retreive all Companies
        [Route("~/api/Company/GetCompanies")]
        [HttpGet]
        public ActionResult<List<Company>> GetCompanies()
        {
            var users = _companyService.GetCompanies();
            return users;
        }

        //Create new company
        [Route("~/api/Company/CreateCompany")]
        [HttpPost]
        public void CreateCompany([FromBody] Company company)
        {
            _companyService.CreateCompany(company);
        }

        //Remove a company
        [Route("~/api/Company/{id}")]
        [HttpDelete("{id}")]
        public void DeleteCompany(int id)
        {
            _companyService.DeleteCompany(id);
        }

        //Update existing company
        [Route("~/api/Company/{id}")]
        [HttpPut("{id}")]
        public void UpdateCompany(int id, [FromBody] Company company)
        {
            _companyService.UpdateCompany(id, company);
        }
    }
}
