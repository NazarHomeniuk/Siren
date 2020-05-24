using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Siren.Contracts.Models.Chat;
using Siren.Contracts.Models.Identity;
using Siren.MobileAppService.Interfaces.Services;

namespace Siren.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;
        private readonly UserManager<User> userManager;

        public ChatController(IChatService chatService, UserManager<User> userManager)
        {
            this.chatService = chatService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpGet("GetConversation")]
        public async Task<IActionResult> GetConversation(int id)
        {
            var conversation = await chatService.GetConversation(id);
            return Ok(conversation);
        }

        [Authorize]
        [HttpGet("GetConversations")]
        public async Task<IActionResult> GetConversations()
        {
            var user = await userManager.GetUserAsync(User);
            var conversations = chatService.GetConversationsForUser(user.Id);
            return Ok(conversations);
        }

        [Authorize]
        [HttpPost("StartConversation")]
        public async Task<IActionResult> StartConversation(string userId)
        {
            var user = await userManager.GetUserAsync(User);
            try
            {
                var result = await chatService.StartConversation(new List<string>(new[] {user.Id, userId}));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Authorize]
        [HttpPost("SendMassage")]
        public async Task<IActionResult> SendMessage(Message message)
        {
            try
            {
                var result = await chatService.AddMessage(message);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}