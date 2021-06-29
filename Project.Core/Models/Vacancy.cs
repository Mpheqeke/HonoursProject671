using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System.Linq;

namespace Project.Core.Models
{
    public class Vacancy : BaseModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int SkillRequirementId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string RefCode { get; set; }
        public string Location { get; set; }
        public string Responsibilities { get; set; } 
        public string DocumentUploadUrl { get; set; }
        public bool ISActive { get; set; }
        public DateTime StartDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy {get;set;}
        public DateTime ModifiedOn { get; set; }


    }
}
