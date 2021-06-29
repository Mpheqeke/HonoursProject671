using Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Project.Core.DTOs;
using System.Threading.Tasks;


namespace Project.Core.Interfaces
{
    public interface IAuthenticationHelper
    {
        Task<UserAuthenticationResponse> SignUpUser(string email, string password);
        Task<SetAccountInfoResponse> SetAccountInfo(string idToken, string firstName, string lastName);
        Task<GetAccountInfoResponse> GetAccountInfo(string idToken);
        Task<UserAuthenticationResponse> VerifyPassword(string email, string password);
        Task<UserAuthenticationResponse> DeleteAccount(string localId);
        Task<SetAccountInfoResponse> ResetPassword(string newPassword, string oldPassword, string email);
       // void updateUser(UserDTO user);
    }
}
