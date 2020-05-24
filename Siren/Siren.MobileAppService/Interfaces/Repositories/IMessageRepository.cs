using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siren.Contracts.Models.Chat;

namespace Siren.MobileAppService.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        IQueryable<Message> GetAll();
        Task<Message> Get(int id);
        Task<Message> Create(Message message);
        Task<Message> Update(Message message);
        Task Delete(int id);
        Task DeleteAll(IEnumerable<Message> messages);
    }
}