using Project.Core.DTOs;
using Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.Interfaces
{
    public interface ICompanyService
    {
        List<VacanciesDTO> GetVacancies(string sort);
        List<Company> GetCompanies();
        List<VacanciesDTO> SearchVacancies(string sort, string search);
        void CreateCompany(Company company);
        void DeleteCompany(int id);
        void UpdateCompany(int id, Company company);
        List<CompanySpecificVacanciesDTO> GetCompanyVacancies(int id);
        List<SpecificVacancyDetailsDTO> GetVacancyInfo(int id);
    }
}
