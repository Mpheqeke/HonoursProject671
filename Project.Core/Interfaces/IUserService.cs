using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Project.Core.DTOs;
using Project.Core.Models;
using Project.Core.Services;

namespace Project.Core.Interfaces
{
    public interface IUserService
    {
        Task UploadProfileImage(IFormFile file, int userId); //WERK
        Task UploadCVDocument(IFormFile file, int userId); //WERK
        Task UploadCourseCert(IFormFile file, int userId); //WERK
        Task DeleteCourseCert(int docId); //WERK
        string GetUserProfilePicture(int userId);
        string GetUserDocument(int docId);
        List<CourseCertDTOcs> GetUserCourseCertificates(int userId);


        //***User(Graduate/Recruiter) CRUD Operations***

        //-->A user can update their profile information
        void UpdateUser(int userId, User user);

            //-->A user can delete their own profile (or deactive profiles can be deleted)
        void DeleteUser(int userId);

            //-->Gets profile information of a specific user
        List<UserDTO> GetSpecificUser(int userId);
       

        //***Moocs Operations***
        //-->Gets all of the Moocs (Moocs Table / From user profile)
        List<MoocsDTO> GetMoocs();

            //-->Allows users to search for Moocs
        List<MoocsDTO> SearchMoocs(string search);


        //***JobApplication Queries***
            //-->A user can apply to any open vacancy (From Company Profile pages)
        void ApplyToPosition(int userId, int vacId, UserJobApplication application);

            //-->A user can view all of their own applications
        List<UserApplicationsDTO> GetApplications(int userId);

            //-->A user can view the details of one of their applications
        List<UserAppliDetailsDTO> ViewApplication(int userId, int applicationId);

            //-->A user can delete their own application
        void DeleteApplication(int applicationId);


        //***User Related Select Operations*** (Might not use)
        List<User> GetUsers();
        List<User> GetGrads();
        List<User> GetSingleUser(int userId);

    }
}