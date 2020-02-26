using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Library;
using Newtonsoft.Json;
using Siren.Contracts.Services;

namespace Siren.Services
{
    public class AudioService : IAudioService
    {
        private readonly IHttpService httpService;

        public AudioService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<IMediaItem> PlayAll()
        {
            var response = await httpService.GetAsync("Audio/GetAllIds", App.Token);
            var ids = JsonConvert.DeserializeObject<IEnumerable<int>>(await response.Content.ReadAsStringAsync());
            var audioList = ids.Select(i => App.ApiUrl + $"Audio/GetTrack?id={i}");
            var track = await CrossMediaManager.Current.Play(audioList);
            return track;
        }

        public async Task<IMediaItem> Play(int trackId)
        {
            var track = await CrossMediaManager.Current.Play(App.ApiUrl + $"Audio/GetTrack?id={trackId}");
            return track;
        }

        public async Task Pause()
        {
            await CrossMediaManager.Current.Pause();
        }
    }
}
