using System.Collections.Generic;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Library;
using Newtonsoft.Json;
using Siren.Contracts.Models.Profile;
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

        public async Task<IEnumerable<int>> GetAllTrackIds()
        {
            var response = await httpService.GetAsync("Audio/GetAllIds", App.Token);
            var ids = JsonConvert.DeserializeObject<IEnumerable<int>>(await response.Content.ReadAsStringAsync());
            return ids;
        }

        public async Task<IEnumerable<Track>> GetAllTracks()
        {
            var response = await httpService.GetAsync("Audio/GetAll", App.Token);
            var tracks = JsonConvert.DeserializeObject<IEnumerable<Track>>(await response.Content.ReadAsStringAsync());
            return tracks;
        }

        public async Task<IMediaItem> Play(int trackId)
        {
            var track = await CrossMediaManager.Current.Play(App.ApiUrl + $"Audio/PlayTrack?id={trackId}");
            return track;
        }

        public async Task Pause()
        {
            await CrossMediaManager.Current.Pause();
        }
    }
}
