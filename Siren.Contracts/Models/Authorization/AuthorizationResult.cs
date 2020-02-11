namespace Siren.Contracts.Models.Authorization
{
    public class AuthorizationResult
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }
}
