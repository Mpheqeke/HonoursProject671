using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System.Linq;

namespace Project.Core.Models
{
    public class Skill : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<UserJobApplication> UserJobApplications { get; set; }
        public virtual List<UserSkillGain> UserSkillGains { get; set; }
    }
}
