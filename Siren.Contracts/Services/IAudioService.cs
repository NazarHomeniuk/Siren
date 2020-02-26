using System.Threading.Tasks;
using MediaManager.Library;

namespace Siren.Contracts.Services
{
    public interface IAudioService
    {
        Task<IMediaItem> PlayAll();
        Task<IMediaItem> Play(int trackId);
        Task Pause();
    }
}
