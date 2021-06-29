using Project.Core.DTOs;
using Project.Core.Global;
using Project.Core.Interfaces;
using Project.Core.Models;
using Project.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Project.Core.Services
{
    public class AuthInfo : IAuthInfo
    {
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public List<string> Permissions { get; set; }
        public int SiteId { get; set; }
    }
}