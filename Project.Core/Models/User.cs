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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string UUID { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        
        public bool IsSuperAdmin { get; set; }
        public string DisplayName => $"{FirstName} {LastName}";
        
        //keys
        public virtual List<CompanyRepresentative> CompanyRepresentatives { get; set; }
        public virtual List<UserPermission> UserPermissions { get; set; }
        public virtual List<UserDocument> UserDocuments { get; set; }
        public virtual List<UserSkillGain> UserSkillGains { get; set; }
        public virtual List<UserJobApplication> UserJobApplications { get; set; }
        public virtual Role Role { get; set; }
    }
}
