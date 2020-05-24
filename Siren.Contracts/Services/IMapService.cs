using System.Collections.Generic;
using System.Threading.Tasks;
using Siren.Contracts.Models.Map;

namespace Siren.Contracts.Services
{
    public interface IMapService
    {
        Task<UserMapInfo> UpdateUserPosition(double longitude, double latitude);
        Task<IEnumerable<UserMapInfo>> GetMapUsers();
    }
}