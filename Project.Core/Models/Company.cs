using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System.Linq;

namespace Project.Core.Models
{
    public class Company : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
        public string Vision { get; set; }
        public string Mission { get; set; }
        public bool IsActive { get; set; }
        public string LogoUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy {get;set;}
        public DateTime ModifiedOn { get; set; }

        //keys
        public virtual List<CompanyRepresentative> CompanyRepresentatives { get; set; }
        public virtual List<Vacancy> Vacancy { get; set; }

    }
}