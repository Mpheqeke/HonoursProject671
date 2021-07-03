using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System.Linq;

namespace Project.Core.Models
{
    public class UserSkillGain : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MoocsId { get; set; }
        public int SkillId { get; set; }
        public int DocumentId { get; set; }

        public virtual User User { get; set; }
        public virtual Moocs Moocs { get; set; }
        public virtual Skill Skill{ get; set; }
        public virtual UserDocument UserDocument { get; set; }
    }
}
