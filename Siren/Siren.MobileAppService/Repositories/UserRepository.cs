using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Siren.Contracts.Models.Identity;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Models;

namespace Siren.MobileAppService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<User> GetAll()
        {
            return dataContext.Users;
        }

        public async Task<User> GetWithAdditionalInfo(string id)
        {
            return await dataContext.Users.Include(u => u.Track).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> Update(User user)
        {
            var result = dataContext.Users.Update(user);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
