using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siren.Contracts.Models.Chat;

namespace Siren.MobileAppService.Interfaces.Repositories
{
    public interface IConversationUserRepository
    {
        IQueryable<ConversationUser> GetAll();
        Task<ConversationUser> Get(int id);
        Task<ConversationUser> Create(ConversationUser conversationUser);
        Task CreateRange(IEnumerable<ConversationUser> conversationUsers);
        Task<ConversationUser> Update(ConversationUser conversationUser);
        Task Delete(int id);
        Task DeleteAll(IEnumerable<ConversationUser> conversationUser);
    }
}