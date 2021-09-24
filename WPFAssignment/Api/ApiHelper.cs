using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WPFAssignment.Api
{
    public class ApiHelper
    {
        /// <summary>
        /// Send a GET request to the given Uri
        /// </summary>
        public async static Task<HttpResponseMessage> GetCall(string apiUrl)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(600);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var httpResponseMessage = await httpClient.GetAsync(apiUrl);

                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
