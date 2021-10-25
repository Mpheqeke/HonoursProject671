using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

        [Route("~/api/Company/GetCompanyFromRepId/{repId}")]
        [HttpGet]
        public int GetCompanyIdFromRep(int repId)
        {
            return _companyService.GetCompanyIdFromRep(repId);
        }

        #region Logo Image Queries
        //Upload Profile Image / Replace Exisiting One
        [Route("~/api/Company/UploadCompanyLogo/{compId}")]
        [HttpPut("{compId}")]
        public async Task UploadCompanyLogo(int compId, [FromForm] IFormFile file)
        {
            try
            {
                await _companyService.UploadCompanyLogo(compId, file);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        //Get profile picture of specific company
        [Route("~/api/Company/GetImage/{compId}")]
        [HttpGet("{compId}")]
        public void GetImage(int compId)
        {
            var url = _companyService.GetWorkingUrl(compId);
            Response.Redirect(url);
        }

        //For reference to get files
        //"https://storage.googleapis.com/BUCKET_NAME/OBJECT_NAME"
        #endregion

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

        //Remove a company
        [Route("~/api/Company/DeleteCompany/{compId}")]
        [HttpDelete("{compId}")]
        public void DeleteCompany(int compId)
        {
            _companyService.DeleteCompany(compId);
        }

        //Update existing company
        [Route("~/api/Company/UpdateCompany/{compId}")]
        [HttpPut("{compId}")]
        public void UpdateCompany(int compId,  Company company)
        {
            _companyService.UpdateCompany(compId, company);
        }
        #endregion

        #region Company Recruiters (View Available, Add New, Remove Exisiting)

        //View all available recruiters
        [Route("~/api/Company/GetAvailableRecruiters")]
        [HttpGet]
        public ActionResult<List<RecruitersDTO>> GetAvailableRecruiters(int? pageNumber)
        {
            int curPage = pageNumber ?? 1;
            int curPageSize = 30;

            var users = _companyService.GetAvailableRecruiters();
            return Ok(users.Skip((curPage - 1) * curPageSize).Take(curPageSize));
        }

        //Allow company to add new representative
        [Route("~/api/Company/AddNewCompanyRep/{compId}/{userId}")]
        [HttpPut("{userId}")]
        public void AddNewCompanyRep(int compId, int userId)
        {
            _companyService.AddNewCompanyRep(compId, userId);
        }

        //Allow company to remove exisiting representative
        [Route("~/api/Company/RemoveCompanyRep/{repId}")]
        [HttpDelete("{repId}")]
        public void RemoveCompanyRep(int repId)
        {
            _companyService.RemoveCompanyRep(repId);
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
        [Route("~/api/Vacancy/{vacId}")]
        [HttpGet("{vacId}")]
        public ActionResult<List<SpecificVacancyDetailsDTO>> GetVacancyInfo(int vacId)
        {
            var vacancy = _companyService.GetVacancyInfo(vacId);
            return vacancy;
        }

        //Retreive all vacancies by company id
        [Route("~/api/Vacancy/Company/{compId}")]
        [HttpGet("{compId}")]
        public ActionResult<List<CompanySpecificVacanciesDTO>> GetCompanyVacancies(int compId)
        {
            var vacancy = _companyService.GetCompanyVacancies(compId);
            return vacancy;
        }
        #endregion

        #region Vacancy CRUD Related Queries (Create, Update, Delete)
        //Create new position
        [Route("~/api/Company/Vacancy/CreatePosition/{CompId}")]
        [HttpPost]
        public void CreatePosition( Vacancy vacancy)
        {
            _companyService.CreatePosition(vacancy, vacancy.CompanyId);
        }

        //Remove a position
        [Route("~/api/Company/Vacancy/DeletePosition/{VacId}")]
        [HttpPut("{VacId}")]
        public void DeletePosition(int VacId)
        {
            _companyService.DeletePosition(VacId);
        }

        //Update existing position
        [Route("~/api/Company/Vacancy/UpdatePosition/{CompId}/{VacId}")]
        [HttpPut("{VacId}")]
        public void UpdatePosition(int VacId, int CompId,  Vacancy vacancy)
        {
            _companyService.UpdatePosition(VacId, CompId, vacancy);
        }
        #endregion

        #region User Application Related Queries 
        //View all applications for specific company position
        [Route("~/api/Company/GetPositionApplicants/{VacId}")]
        [HttpGet("{VacId}")]
        public ActionResult<List<CompanyApplicantsDTO>> GetPositionApplicants(int VacId)
        {
            var applicants = _companyService.GetPositionApplicants(VacId);
            return applicants;
        }

        //View specific applicant profile (Sets Status = Viewed)
        [Route("~/api/Company/Vacancy/ViewSpecificApplicantProfile/{compId}/{userId}")]
        [HttpGet("{userId}")]
        public ActionResult<List<UserDTO>> ViewSpecificApplicantProfile(int userId, int compId)
        {
            var applicantProfile = _companyService.ViewSpecificApplicantProfile(userId, compId);
            return applicantProfile;
        }

        //Approve an application
        [Route("~/api/Company/Vacancy/ApproveApplication/{CompId}/{appliId}")]
        [HttpPut("{appliId}")]
        public void ApproveApplication(int compId, int appliId, [FromBody] UserJobApplication application)
        {
            _companyService.ApproveApplication(compId, appliId, application);
        }

        //Reject an application
        [Route("~/api/Company/Vacancy/RejectApplication/{CompId}/{appliId}")]
        [HttpPut("{appliId}")]
        public void RejectApplication(int compId, int appliId, [FromBody] UserJobApplication application)
        {
            _companyService.RejectApplication(compId, appliId, application);
        }
        #endregion

    }
}
