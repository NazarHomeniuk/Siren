using System.Collections.Generic;
using System.Threading.Tasks;
using Siren.Contracts.Models.Profile;

namespace Siren.MobileAppService.Interfaces.Services
{
    public interface ITrackService
    {
        Task<Track> Get(int id);
        IEnumerable<int> GetAllIds();
        Task Upload(string path);
    }
}