using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.DTOs
{
    public class SpecificVacancyDetailsDTO
    {
        //Company Table
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Sector { get; set; }

        //Vacancy Table
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public string Responsibilities { get; set; }

        //Skill Table
        public string SkillName { get; set; }
    }
}
