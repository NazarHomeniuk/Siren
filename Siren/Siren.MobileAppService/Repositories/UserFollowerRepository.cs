using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siren.Contracts.Models.Profile;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Models;

namespace Siren.MobileAppService.Repositories
{
    public class UserFollowerRepository : IUserFollowerRepository
    {
        private readonly DataContext dataContext;

        public UserFollowerRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<UserFollower> GetAll()
        {
            return dataContext.UserFollowers;
        }

        public async Task<UserFollower> Get(int id)
        {
            return await dataContext.UserFollowers.FindAsync(id);
        }

        public async Task<UserFollower> Create(UserFollower userFollower)
        {
            var result = await dataContext.UserFollowers.AddAsync(userFollower);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<UserFollower> Update(UserFollower userFollower)
        {
            var result = dataContext.UserFollowers.Update(userFollower);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(int id)
        {
            var follower = await dataContext.UserFollowers.FindAsync(id);
            dataContext.UserFollowers.Remove(follower);
            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteAll(IEnumerable<UserFollower> userFollowers)
        {
            dataContext.UserFollowers.RemoveRange(userFollowers);
            await dataContext.SaveChangesAsync();
        }
    }
}
