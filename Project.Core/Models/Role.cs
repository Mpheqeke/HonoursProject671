using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System.Linq;

namespace Project.Core.Models
{
    public class Role : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<RolePermission> RolePermissions { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
