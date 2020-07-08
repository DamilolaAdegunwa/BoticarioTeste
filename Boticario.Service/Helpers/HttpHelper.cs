using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http.Headers;

namespace Boticario.Service.Helpers
{
    public static class HttpHelper
    {        
        public static async Task<T> Get<T>(string apiBasicUri, string url, string token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBasicUri);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                return resultContent;
            }
        }
    }
}
