using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System.Linq;

namespace Project.Core.Models
{
    public class UserJobApplication : BaseModel
    {
      
        public int Id { get; set; }
        public int UserId { get; set; }

        public int VacancyId { get; set; }
        public int StatusId { get; set; }
        public int SkillId { get; set; }
        public string Motivation { get; set; }
        public string CVUrl { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual Vacancy Vacancy { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
    }
}
