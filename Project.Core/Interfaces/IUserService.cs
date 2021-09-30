using System.Collections.Generic;
using Project.Core.DTOs;
using Project.Core.Models;
using Project.Core.Services;

namespace Project.Core.Interfaces
{
    public interface IUserService
    {
        //***User(Graduate/Recruiter) CRUD Operations***
            //-->Create a new user
        void CreateUser(User user);

            //-->A user can update their profile information
        void UpdateUser(int id, User user);

            //-->A user can delete their own profile (or deactive profiles can be deleted)
        void DeleteUser(int id);

            //-->Gets profile information of a specific user
        List<UserDTO> GetSpecificUser(int userId);

        //***Moocs Operations***
            //-->Gets all of the Moocs (Moocs Table / From user profile)
        List<MoocsDTO> GetMoocs();

            //-->Allows users to search for Moocs
        List<MoocsDTO> SearchMoocs(string search);


        //***User Related Select Operations*** (Might not use)
        List<User> GetUsers();
        List<User> GetGrads();
        List<User> GetRecruiters();
        List<User> GetSingleUser(int id);


        //***JobApplication Queries***
            //-->A user can apply to any open vacancy (From Company Profile pages)
        void ApplyToPosition(int userId, int vacId, UserJobApplication application);

            //-->A user can view all of their own applications
        List<UserApplicationsDTO> GetApplications(int userId);

            //-->A user can view the details of one of their applications
        List<UserAppliDetailsDTO> ViewApplication(int userId, int applicationId);

            //-->A user can delete their own application
        void DeleteApplication(int applicationId);

    }
}
