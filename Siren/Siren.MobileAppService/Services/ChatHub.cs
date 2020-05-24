using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Siren.Contracts.Models.Identity;

namespace Siren.MobileAppService.Services
{
    public class ChatHub : Hub
    {
        private readonly UserManager<User> userManager;

        public ChatHub(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize]
        public async Task Send(string username, string message, string userId)
        {
            await Clients.User(userId).SendAsync("Receive", username, message);
        }
    }
}
