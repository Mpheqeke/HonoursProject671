using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System.Linq;

namespace Project.Core.Models
{
    public class DocumentType : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //keys
        public virtual List<UserDocument> UserDocuments { get; set; }
    }
}
