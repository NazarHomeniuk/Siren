using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Library;
using Siren.Contracts.Services;
using Siren.Models.Player;
using Xamarin.Forms;

namespace Siren.Services
{
    public class PlayerService
    {
        public ImageSource Image { get; set; }
        public ImageSource PlayImage { get; set; }
        public ImageSource RepeatImage { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public bool IsLoaded { get; set; }
        public int Position { get; set; }
        public int PrevPosition { get; set; }
        public int Duration { get; set; }
        public string PositionTimeSpan { get; set; }
        public string DurationTimeSpan { get; set; }
        public int Volume { get; set; }
        public Track CurrentTrack { get; set; }
        public LinkedList<Track> Tracks { get; set; }

        private readonly IAudioService audioService;

        public PlayerService(IAudioService audioService)
        {
            this.audioService = audioService;
            Init();
        }

        private async void Init()
        {
            var trackIds = await audioService.GetAllTrackIds();
            Tracks = new LinkedList<Track>(trackIds.Select(i => new Track
                {Id = i, Url = App.ApiUrl + $"Audio/PlayTrack?id={i}"}));
        }

        public async Task<IMediaItem> Play(int trackId)
        {
            var track = Tracks.FirstOrDefault(t => t.Id == trackId);
            if (track == null) return null;

            CurrentTrack = track;
            var mediaItem = await CrossMediaManager.Current.Extractor.CreateMediaItem(CurrentTrack.Url);
            return await CrossMediaManager.Current.Play(mediaItem);

        }

        public async Task<IMediaItem> PlayPause()
        {
            if (CurrentTrack == null)
            {
                CurrentTrack = Tracks.First();
                var mediaItem = await CrossMediaManager.Current.Extractor.CreateMediaItem(CurrentTrack.Url);
                return await CrossMediaManager.Current.Play(mediaItem);
            }

            if (CrossMediaManager.Current.IsPlaying())
            {
                await CrossMediaManager.Current.Pause();
            }
            else
            {
                await CrossMediaManager.Current.Play();
            }

            return null;
        }

        public async Task<IMediaItem> Next()
        {
            var track = Tracks.Find(CurrentTrack);
            var nextTrack = track?.Next?.Value;
            if (nextTrack != null)
            {
                CurrentTrack = nextTrack;
                return await CrossMediaManager.Current.Play(
                    await CrossMediaManager.Current.Extractor.CreateMediaItem(CurrentTrack.Url));
            }

            CurrentTrack = Tracks.First();
            return await CrossMediaManager.Current.Play(
                await CrossMediaManager.Current.Extractor.CreateMediaItem(CurrentTrack.Url));
        }

        public async Task<IMediaItem> Prev()
        {
            var track = Tracks.Find(CurrentTrack);
            var prevTrack = track?.Previous?.Value;
            if (prevTrack != null)
            {
                CurrentTrack = prevTrack;
                return await CrossMediaManager.Current.Play(
                    await CrossMediaManager.Current.Extractor.CreateMediaItem(CurrentTrack.Url));
            }

            CurrentTrack = Tracks.Last();
            return await CrossMediaManager.Current.Play(
                await CrossMediaManager.Current.Extractor.CreateMediaItem(CurrentTrack.Url));
        }
    }
}
