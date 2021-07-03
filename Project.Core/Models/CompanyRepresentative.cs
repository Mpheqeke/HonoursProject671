using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System.Linq;

namespace Project.Core.Models
{
    public class CompanyRepresentative : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }

        //keys
        public virtual User User { get; set; }
        public virtual Company Company { get; set; }
    }
}
