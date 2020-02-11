using System.Net.Http;
using System.Threading.Tasks;

namespace Siren.Contracts.Services
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> GetAsync(string url, string token = null);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent request, string token = null);
    }
}
