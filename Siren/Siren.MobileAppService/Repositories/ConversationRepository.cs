using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Siren.Contracts.Models.Chat;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Models;

namespace Siren.MobileAppService.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly DataContext dataContext;

        public ConversationRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Conversation> GetAll()
        {
            return dataContext.Conversations.Include(c => c.Messages).Include(c => c.Participants)
                .ThenInclude(c => c.User);
        }

        public async Task<Conversation> Get(int id)
        {
            return await dataContext.Conversations.Include(c => c.Messages).Include(c => c.Participants)
                .ThenInclude(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Conversation> GetByHashCode(int hashCode)
        {
            return await dataContext.Conversations.Include(c => c.Messages).Include(c => c.Participants)
                .FirstOrDefaultAsync(c => c.HashCode == hashCode);
        }

        public async Task<Conversation> Create(Conversation conversation)
        {
            var result = await dataContext.Conversations.AddAsync(conversation);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Conversation> Update(Conversation conversation)
        {
            var result = dataContext.Conversations.Update(conversation);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(int id)
        {
            var conversation = await dataContext.Conversations.FindAsync(id);
            dataContext.Conversations.Remove(conversation);
            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteAll(IEnumerable<Conversation> conversations)
        {
            dataContext.Conversations.RemoveRange(conversations);
            await dataContext.SaveChangesAsync();
        }
    }
}