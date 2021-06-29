using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System.Linq;

namespace Project.Core.Models
{
    public class RolePermission : BaseModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId{ get; set; }

        public virtual Role Role { get; set; }
    }
}
