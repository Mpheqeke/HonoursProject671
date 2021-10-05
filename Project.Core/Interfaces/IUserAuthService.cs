using Project.Core.DTOs;
using Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.Interfaces
{
    public interface IUserAuthService
    {
        int SingUpUser(User user);
        int SingUpCompany(Company user);
        List<UserDTO> SignIn(string uuid);

    }
}
