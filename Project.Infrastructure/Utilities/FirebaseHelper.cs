using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using FirebaseAdmin;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Core.DTOs;
using Project.Core.Interfaces;
using Project.Core.Utilities;

namespace Project.Infrastructure.Utilities
{
    public class FirebaseHelper: IFireBaseHelper
    {
        private IOptions<FireBase> _appSettings;

        public FirebaseHelper(IOptions<FireBase> settings)
        {
            _appSettings = settings;
        }
        public AuthResponseDTO SingUp(FireBaseAuthDTO signUp)
        {
            var client = new HttpClient();
           
            try
            {
                var response = client.PostAsync(_appSettings.Value.BaseUrl + "/v1/accounts:signUp?key=" + _appSettings.Value.Key,
                                                new StringContent(signUp.ToString()));
                var result = JsonConvert.DeserializeObject<AuthResponseDTO>(response.Result.ToString());

                return result;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return null;
            }

        }

        public AuthResponseDTO SingIn(FireBaseAuthDTO signIn)
        {
            var client = new HttpClient();

            try
            {
                var response = client.PostAsync(_appSettings.Value.BaseUrl + "/v1/accounts:signInWithPassword?key=" + _appSettings.Value.Key,
                                                new StringContent(signIn.ToString()));
                var result = JsonConvert.DeserializeObject<AuthResponseDTO>(response.Result.ToString());

                return result;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
    }
}
