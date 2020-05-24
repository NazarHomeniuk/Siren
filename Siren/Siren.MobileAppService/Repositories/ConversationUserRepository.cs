using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Siren.Contracts.Models.Chat;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Models;

namespace Siren.MobileAppService.Repositories
{
    public class ConversationUserRepository : IConversationUserRepository
    {
        private readonly DataContext dataContext;

        public ConversationUserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<ConversationUser> GetAll()
        {
            return dataContext.ConversationUsers.Include(c => c.Conversation).Include(u => u.User);
        }

        public async Task<ConversationUser> Get(int id)
        {
            return await dataContext.ConversationUsers.FindAsync(id);
        }

        public async Task CreateRange(IEnumerable<ConversationUser> conversationUsers)
        {
            await dataContext.AddRangeAsync(conversationUsers);
            await dataContext.SaveChangesAsync();
        }

        public async Task<ConversationUser> Create(ConversationUser conversationUser)
        {
            var result = await dataContext.ConversationUsers.AddAsync(conversationUser);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ConversationUser> Update(ConversationUser conversationUser)
        {
            var result = dataContext.ConversationUsers.Update(conversationUser);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(int id)
        {
            var conversationUser = await dataContext.ConversationUsers.FindAsync(id);
            dataContext.ConversationUsers.Remove(conversationUser);
            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteAll(IEnumerable<ConversationUser> conversationUser)
        {
            dataContext.ConversationUsers.RemoveRange(conversationUser);
            await dataContext.SaveChangesAsync();
        }
    }
}
