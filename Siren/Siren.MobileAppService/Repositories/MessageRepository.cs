using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siren.Contracts.Models.Chat;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Models;

namespace Siren.MobileAppService.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext dataContext;

        public MessageRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Message> GetAll()
        {
            return dataContext.Messages;
        }

        public async Task<Message> Get(int id)
        {
            return await dataContext.Messages.FindAsync(id);
        }

        public async Task<Message> Create(Message message)
        {
            var result = await dataContext.Messages.AddAsync(message);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Message> Update(Message message)
        {
            var result = dataContext.Messages.Update(message);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(int id)
        {
            var message = await dataContext.Messages.FindAsync(id);
            dataContext.Messages.Remove(message);
            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteAll(IEnumerable<Message> messages)
        {
            dataContext.Messages.RemoveRange(messages);
            await dataContext.SaveChangesAsync();
        }
    }
}
