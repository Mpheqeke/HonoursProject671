using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class VacanciesDTO
    {
        // Id to be able to redirect to the company page when selecting a company
        public int CompanyId { get; set; }

        // Info for Vacancies Table
        public string VacancyName { get; set; }
        public string Sector { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public int TotalVac { get; set; }
    }
}
