using System.Collections.Generic;
using Project.Core.DTOs;
using Project.Core.Models;
using Project.Core.Services;

namespace Project.Core.Interfaces
{
    public interface IUserService
    {

        List<User> GetSingleUser(int id);
        List<User> GetUsers();
        List<User> GetGrads();
        List<User> GetRecruiters();
        void CreateUser(User user);
        void UpdateUser(int id, User user);
        void DeleteUser(int id);


    }
}
