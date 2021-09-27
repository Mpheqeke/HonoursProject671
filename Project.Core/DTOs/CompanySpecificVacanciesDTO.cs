using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class CompanySpecificVacanciesDTO
    {
        // Id to be able to retreive more info on selected vacancy
        public int VacancyId { get; set; }

        // Info for company vacancies on company page
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
    }
}
