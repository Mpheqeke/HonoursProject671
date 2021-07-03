using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System.Linq;

namespace Project.Core.Models
{
    public class User : BaseModel
    {
        public int Id { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public bool IsActive { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string DisplayName => $"{UserFirstName} {UserLastName}";
        
        //keys
        public virtual List<CompanyRepresentative> CompanyRepresentatives { get; set; }
        public virtual List<UserPermission> UserPermissions { get; set; }
        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<UserDocument> UserDocuments { get; set; }
        public virtual List<UserSkillGain> UserSkillGains { get; set; }
    }
}
