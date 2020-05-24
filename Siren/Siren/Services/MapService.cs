using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Siren.Contracts.Models.Map;
using Siren.Contracts.Services;

namespace Siren.Services
{
    public class MapService : IMapService
    {
        private readonly IHttpService httpService;

        public MapService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<UserMapInfo> UpdateUserPosition(double longitude, double latitude)
        {
            var position = new UserPosition
            {
                Longitude = longitude,
                Latitude = latitude
            };
            var jsonPosition = JsonConvert.SerializeObject(position);
            var stringContent = new StringContent(jsonPosition, Encoding.UTF8, "application/json");
            var response = await httpService.PostAsync("Map/UpdateCurrentUserPosition", stringContent, App.Token);
            var result = JsonConvert.DeserializeObject<UserMapInfo>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<IEnumerable<UserMapInfo>> GetMapUsers()
        {
            var response = await httpService.GetAsync("Map/GetMapUsers", App.Token);
            var result =
                JsonConvert.DeserializeObject<IEnumerable<UserMapInfo>>(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}
