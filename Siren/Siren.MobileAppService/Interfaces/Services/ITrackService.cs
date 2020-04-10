using System.Collections.Generic;
using System.Threading.Tasks;
using Siren.Contracts.Models.Identity;
using Siren.Contracts.Models.Profile;

namespace Siren.MobileAppService.Interfaces.Services
{
    public interface ITrackService
    {
        Task<Track> Play(int id, User user);
        Task<Track> Get(int id);
        IEnumerable<int> GetAllIds();
        IEnumerable<Track> GetAll();
        Task Upload(string path);
    }
}