using System.Collections.Generic;
using System.Threading.Tasks;
using Siren.Contracts.Models.Chat;

namespace Siren.Contracts.Services
{
    public interface IChatService
    {
        Task<Conversation> GetConversation(int id);
        Task<IEnumerable<Conversation>> GetConversations();
        Task<Conversation> StartConversation(string userId);
        Task SendMessage(Message message);
    }
}