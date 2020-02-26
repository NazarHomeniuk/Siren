using System.Collections.Generic;
using System.Threading.Tasks;
using Siren.Contracts.Models.Profile;

namespace Siren.MobileAppService.Interfaces.Repositories
{
    public interface ITrackRepository
    {
        IEnumerable<Track> GetAll();
        Task<Track> Get(int id);
        Task<Track> Create(Track track);
        Task<Track> Update(Track track);
        Task Delete(int id);
    }
}