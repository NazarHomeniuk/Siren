using System.Collections.Generic;
using System.Threading.Tasks;
using Siren.Contracts.Models.Identity;
using Siren.Contracts.Models.Map;

namespace Siren.MobileAppService.Interfaces.Services
{
    public interface IMapService
    {
        Task<UserMapInfo> UpdateUserPosition(User user, UserPosition userPosition);
        IEnumerable<UserMapInfo> GetMapUsers();
    }
}