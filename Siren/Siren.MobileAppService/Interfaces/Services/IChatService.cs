using System.Collections.Generic;
using System.Threading.Tasks;
using Siren.Contracts.Models.Chat;

namespace Siren.MobileAppService.Interfaces.Services
{
    public interface IChatService
    {
        IEnumerable<Conversation> GetConversationsForUser(string userId);
        Task<Conversation> GetConversation(int id);
        Task<Conversation> StartConversation(List<string> userIds);
        Task<Message> AddMessage(Message message);
    }
}