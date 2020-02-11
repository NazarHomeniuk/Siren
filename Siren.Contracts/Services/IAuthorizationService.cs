using System.Threading.Tasks;
using Siren.Contracts.Models.Authorization;

namespace Siren.Contracts.Services
{
    public interface IAuthorizationService
    {
        Task<AuthorizationResult> Login(LoginRequest loginRequest);
        Task<AuthorizationResult> SignUp(SignUpRequest signUpRequest);
        Task Logout();
    }
}