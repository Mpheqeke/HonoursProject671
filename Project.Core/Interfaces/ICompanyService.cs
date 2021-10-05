using Project.Core.DTOs;
using Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.Interfaces
{
    public interface ICompanyService
    {
        //***Company CRUD Operations***
            //-->Gets Profile information of a specific company (Company Profile Page)
        List<CompanyDTO> GetSpecificCompany(int CompId);

            //-->Gets a List of all companies (NOT NEEDED maybe?)
        List<Company> GetCompanies();

            //-->A company can delete their own profile (or deactive profiles can be deleted)
        void DeleteCompany(int compId);

            //-->A company can update their own profile information
        void UpdateCompany(int compId, Company company);


        //***Company Representative Operations***
            //-->Gets all reps for a specific company
        List<CompanyRepsDTO> GetCompSpecificReps(int CompId);

            //-->Gets all of the available recruiters(so company can add them as their own recruiter)
        List<RecruitersDTO> GetAvailableRecruiters();

            //-->A company can add a recruiter to represent them
        void AddNewCompanyRep(int compId, int userId);

            //-->A company can remove a recruiter that currently represents them
        void RemoveCompanyRep(int repId);


        //***Vancancy CRUD Operations***
        //-->Gets all the vacancies for a specific company (Company Profile Page)
        List<CompanySpecificVacanciesDTO> GetCompanyVacancies(int compId);

            //-->Gets the details for a select vancancy (Company Profile Page)
        List<SpecificVacancyDetailsDTO> GetVacancyInfo(int vacId);

            //-->Gets all vacancies from all companies (Vacancies Table/Page)
        List<VacanciesDTO> GetVacancies(string sort);

            //-->Allows Users to search for vancacnies (Vacancies Table/Page)
        List<VacanciesDTO> SearchVacancies(string sort, string search);

            //-->A company can create a new position
        void CreatePosition(Vacancy vacancy, int CompId);

            //->A company can update a positions informations
        void UpdatePosition(int VacId, int CompId, Vacancy vacancy);

            //-->A company can remove a position 
        void DeletePosition(int VacId);


        //***JobApplication Operations***
            //-->A company can view all applications for a position
        List<CompanyApplicantsDTO> GetPositionApplicants(int vacId);

            //-->A company can approve an application
        void ApproveApplication(int compId, int appliId, UserJobApplication application);

            //-->A company can reject an application
        void RejectApplication(int compId, int appliId, UserJobApplication application);

            //-->A company can view a selected applicants profile
        List<UserDTO> ViewSpecificApplicantProfile(int userId, int compId);


        //***Company Profile Image Queries***
        //-->A company can upload/change profile image
        void UploadImage(string imagePath, int compId);

        //-->Used to display the image
        byte[] GetImage(int compId);

        //-->Retreives image path
        string GetImagePath(string imagePath, int compId);

    }
}
