using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System.Linq;

namespace Project.Core.Models
{
    public class UserRole : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }


    }
}
