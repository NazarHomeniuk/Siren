using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Siren.Contracts.Models.Authorization;
using Siren.Contracts.Models.Identity;
using Siren.MobileAppService.Configuration;

namespace Siren.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IdentityConfig identityConfig;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AuthorizationController(IOptions<IdentityConfig> identityConfig, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.identityConfig = identityConfig.Value;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [Route("login")]
        [HttpPost]
        public  async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, loginRequest.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok(new AuthorizationResult
                    {
                        IsSuccess = true,
                        Token = GenerateJwtToken(loginRequest.Email, user),
                        UserId = user.Id
                    });
                }
            }

            return Ok(new AuthorizationResult
            {
                IsSuccess = false,
                ErrorMessage = "Invalid email or password"
            });
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
        {
            var user = new User
            {
                UserName = signUpRequest.Name,
                Email = signUpRequest.Email
            };

            var result = await userManager.CreateAsync(user, signUpRequest.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return Ok(new AuthorizationResult
                {
                    IsSuccess = true,
                    Token = GenerateJwtToken(signUpRequest.Email, user),
                    UserId = user.Id
                });
            }

            return Ok(new AuthorizationResult
            {
                IsSuccess = false
            });
        }

        private string GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identityConfig.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(identityConfig.ExpirationDays));

            var token = new JwtSecurityToken(
                identityConfig.Issuer,
                identityConfig.Issuer,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}