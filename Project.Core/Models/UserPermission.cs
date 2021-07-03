using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System.Linq;

namespace Project.Core.Models
{
    public class UserPermission : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PermissionId { get; set; }
      
        public virtual User User { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
