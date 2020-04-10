using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siren.Contracts.Models.Profile;

namespace Siren.MobileAppService.Interfaces.Repositories
{
    public interface IUserTrackRepository
    {
        IQueryable<UserTrack> GetAll();
        Task<UserTrack> Get(int id);
        Task<UserTrack> Create(UserTrack userTrack);
        Task<UserTrack> Update(UserTrack userTrack);
        Task Delete(int id);
        Task DeleteAll(IEnumerable<UserTrack> userTracks);
    }
}