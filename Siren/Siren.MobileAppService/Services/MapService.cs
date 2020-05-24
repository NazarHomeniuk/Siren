using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Siren.Contracts.Models.Identity;
using Siren.Contracts.Models.Map;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Interfaces.Services;

namespace Siren.MobileAppService.Services
{
    public class MapService : IMapService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public MapService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserMapInfo> UpdateUserPosition(User user, UserPosition userPosition)
        {
            user.Latitude = userPosition.Latitude;
            user.Longitude = userPosition.Longitude;
            var result = await userRepository.Update(user);
            var userMapInfo = mapper.Map<UserMapInfo>(result);
            return userMapInfo;
        }

        public IEnumerable<UserMapInfo> GetMapUsers()
        {
            var users = userRepository.GetAll().Where(u => u.IsPositionEnabled);
            var resultUsers = mapper.Map<IEnumerable<UserMapInfo>>(users);
            return resultUsers;
        }
    }
}
