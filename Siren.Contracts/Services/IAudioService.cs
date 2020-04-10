using System.Collections.Generic;
using System.Threading.Tasks;
using MediaManager.Library;
using Siren.Contracts.Models.Profile;

namespace Siren.Contracts.Services
{
    public interface IAudioService
    {
        Task<IEnumerable<int>> GetAllTrackIds();
        Task<IEnumerable<Track>> GetAllTracks();
        Task<IMediaItem> Play(int trackId);
        Task Pause();
    }
}
