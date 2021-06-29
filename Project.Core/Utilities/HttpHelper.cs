using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project.Core.Utilities
{
	public static class HttpHelper
	{
		public static async Task<T> Get<T>(string url)
		{
			using (var client = new HttpClient())
			{
				var result = await client.GetAsync(url);
				var resultContent = await result.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<T>(resultContent);
			}
		}

		public static async Task<T> Post<T>(string url, HttpContent content)
		{
			using (var client = new HttpClient())
			{
				var result = await client.PostAsync(url, content);
				var resultContent = await result.Content.ReadAsStringAsync();

				return JsonConvert.DeserializeObject<T>(resultContent);
			}
		}

	}
}