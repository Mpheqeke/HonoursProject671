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
        List<UserDTO> GetSpecificUser(int userId);
        void CreateUser(User user);
        void UpdateUser(int id, User user);
        void DeleteUser(int id);
        List<MoocsDTO> GetMoocs();
        List<MoocsDTO> SearchMoocs(string search);
        void ApplyToPosition(int userId, int vacId, UserJobApplication application);
    }
}
