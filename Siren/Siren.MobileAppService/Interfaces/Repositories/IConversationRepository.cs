using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siren.Contracts.Models.Chat;

namespace Siren.MobileAppService.Interfaces.Repositories
{
    public interface IConversationRepository
    {
        IQueryable<Conversation> GetAll();
        Task<Conversation> Get(int id);
        Task<Conversation> GetByHashCode(int hashCode);
        Task<Conversation> Create(Conversation conversation);
        Task<Conversation> Update(Conversation conversation);
        Task Delete(int id);
        Task DeleteAll(IEnumerable<Conversation> conversations);
    }
}