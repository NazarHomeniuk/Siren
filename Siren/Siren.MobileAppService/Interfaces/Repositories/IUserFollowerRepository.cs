using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siren.Contracts.Models.Profile;

namespace Siren.MobileAppService.Interfaces.Repositories
{
    public interface IUserFollowerRepository
    {
        
        IQueryable<UserFollower> GetAll();
        Task<UserFollower> Get(int id);
        Task<UserFollower> Create(UserFollower userFollower);
        Task<UserFollower> Update(UserFollower userFollower);
        Task Delete(int id);
        Task DeleteAll(IEnumerable<UserFollower> userFollowers);
    }
}