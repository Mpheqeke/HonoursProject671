using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System.Linq;

namespace Project.Core.Models
{
    public class UserDocument : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DocumentTypeId { get; set; }
        public int StatusId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentUrl { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
        public virtual List<UserSkillGain> UserSkillGains { get; set; }

    }
}
