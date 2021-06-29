using System;
using System.Net;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Project.EmailFunction
{
    public class EmailFunction
    {
        [FunctionName("EmailFunction")]
        public async void Run([TimerTrigger("0 */15 * * * *")]TimerInfo myTimer, ILogger log)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://Projectapi.azurewebsites.net/api/email");
            await request.GetResponseAsync();
        }
    }
}
