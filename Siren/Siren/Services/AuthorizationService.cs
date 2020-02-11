using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Siren.Contracts.Models.Authorization;
using Siren.Contracts.Services;

namespace Siren.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IHttpService httpService;

        public AuthorizationService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<AuthorizationResult> Login(LoginRequest loginRequest)
        {
            var jsonRequest = JsonConvert.SerializeObject(loginRequest);
            var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await httpService.PostAsync("Authorization/Login", stringContent);
            var result = JsonConvert.DeserializeObject<AuthorizationResult>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<AuthorizationResult> SignUp(SignUpRequest signUpRequest)
        {
            var jsonRequest = JsonConvert.SerializeObject(signUpRequest);
            var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await httpService.PostAsync("Authorization/SignUp", stringContent);
            var result = JsonConvert.DeserializeObject<AuthorizationResult>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public Task Logout()
        {
            throw new System.NotImplementedException();
        }
    }
}