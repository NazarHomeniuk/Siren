using Siren.Contracts.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Siren.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient httpClient;

        public HttpService()
        {
            httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string url, string token = null)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync(App.ApiUrl + url);
            return result;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent request, string token = null)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.PostAsync(App.ApiUrl + url, request);
            return result;
        }
    }
}
