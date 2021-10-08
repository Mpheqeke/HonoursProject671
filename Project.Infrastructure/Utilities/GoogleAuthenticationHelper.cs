using Project.Core.Interfaces;
using Project.Core.DTOs;
using Project.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Auth;

namespace Project.Infrastructure.Utilities
{
    public class GoogleAuthenticationHelper
    {
        private IOptions<AppSettings> _appSettings;
        public static string query = "https://www.googleapis.com";
        public static string googleApi = "/identitytoolkit/v3/relyingparty/";

        public GoogleAuthenticationHelper(IOptions<AppSettings> settings)
        {
            _appSettings = settings;
        }

        public async Task<UserAuthenticationResponse> SignUpUser(string email, string password)
        {
            UserAuthenticationResponse _response = new UserAuthenticationResponse();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(query);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("returnSecureToken", "true")
                });

                var result = await client.PostAsync(googleApi + "signupNewUser?key=" + _appSettings.Value.FirebaseApiKey, content);
                var resultContent = await result.Content.ReadAsStringAsync();
                if (!resultContent.Contains("errors"))
                {
                    return _response = AuthenticationResponse(resultContent);
                }

                var error = ErrorResponse(resultContent);
                throw new Exception(error.Error.Message);
            }
        }

        public async Task<SetAccountInfoResponse> SetAccountInfo(string idToken, string firstName, string lastName)
        {
            SetAccountInfoResponse _response = new SetAccountInfoResponse();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(query);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("idToken", idToken),
                    new KeyValuePair<string, string>("displayName", firstName + " " + lastName),
                    new KeyValuePair<string, string>("returnSecureToken", "true")
                });

                var result = await client.PostAsync(googleApi + "setAccountInfo?key=" + _appSettings.Value.FirebaseApiKey, content);
                var resultContent = await result.Content.ReadAsStringAsync();

                return _response = SetAccountInfoResponse(resultContent);
            }
        }
        public async Task<SetAccountInfoResponse> ResetPassword(string newPassword, string oldPassword, string email)
        {
            SetAccountInfoResponse _response = new SetAccountInfoResponse();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(query);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("newPassword", newPassword),
                    new KeyValuePair<string, string>("oldPassword", oldPassword),
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("returnSecureToken", "true")
                });

                var result = await client.PostAsync(googleApi + "resetPassword?key=" + _appSettings.Value.FirebaseApiKey, content);
                var resultContent = await result.Content.ReadAsStringAsync();

                return _response = SetAccountInfoResponse(resultContent);
            }
        }

        public async Task<GetAccountInfoResponse> GetAccountInfo(string idToken)
        {
            GetAccountInfoResponse _response = new GetAccountInfoResponse();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(query);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("idToken", idToken)
                });

                var result = await client.PostAsync(googleApi + "getAccountInfo?key=" + _appSettings.Value.FirebaseApiKey, content);
                var resultContent = await result.Content.ReadAsStringAsync();
                if (!resultContent.Contains("errors"))
                {
                    
                    return _response = GetAccountInfoResponse(resultContent);
                }

                var error = ErrorResponse(resultContent);
                throw new Exception(error.Error.Message);
            }
        }

        public async Task<UserAuthenticationResponse> VerifyPassword(string email, string password)
        {
            UserAuthenticationResponse _response = new UserAuthenticationResponse();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(query);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", email),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("returnSecureToken", "true")
                });

                var result = await client.PostAsync(googleApi + "verifyPassword?key=" + _appSettings.Value.FirebaseApiKey, content);
                var resultContent = await result.Content.ReadAsStringAsync();
                if (!resultContent.Contains("errors"))
                {
                    return _response = AuthenticationResponse(resultContent);
                }

                var error = ErrorResponse(resultContent);
                throw new Exception(error.Error.Message);
            }
        }

        public async Task<UserAuthenticationResponse> DeleteAccount(string localId)
        {
            UserAuthenticationResponse _response = new UserAuthenticationResponse();

           

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(query);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("localId", localId)
                });

                var result = await client.PostAsync(googleApi + "deleteAccount?key=" + _appSettings.Value.FirebaseApiKey, content);
                var resultContent = await result.Content.ReadAsStringAsync();
                if (!resultContent.Contains("errors"))
                {
                    return _response = AuthenticationResponse(resultContent);
                }

                var error = ErrorResponse(resultContent);
                throw new Exception(error.Error.Message);
            }
        }


        


        static UserAuthenticationResponse AuthenticationResponse(string input)
        {
            var result = JsonConvert.DeserializeObject<UserAuthenticationResponse>(input);
            return result;
        }

        static SetAccountInfoResponse SetAccountInfoResponse(string input)
        {
            var result = JsonConvert.DeserializeObject<SetAccountInfoResponse>(input);
            return result;
        }

        static GetAccountInfoResponse GetAccountInfoResponse(string input)
        {
            var result = JsonConvert.DeserializeObject<GetAccountInfoResponse>(input);
            return result;
        }

        static GoogleErrorResponse ErrorResponse(string input)
        {
            var result = JsonConvert.DeserializeObject<GoogleErrorResponse>(input);
            return result;
        }
    }
}