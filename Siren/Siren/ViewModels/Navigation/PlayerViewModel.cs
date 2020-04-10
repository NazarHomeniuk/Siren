using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Android.Graphics;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using MediaManager.Playback;
using MediaManager.Player;
using Siren.Annotations;
using Siren.Services;
using Siren.Views.Navigation;
using Syncfusion.SfRangeSlider.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using PositionChangedEventArgs = MediaManager.Playback.PositionChangedEventArgs;

namespace Siren.ViewModels.Navigation
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class PlayerViewModel : INotifyPropertyChanged
    {
        private const string PlayImageSrc = "play.png";
        private const string PauseImageSrc = "pause.png";
        private const string RepeatImageSrc = "repeat.png";
        private const string RepeatActiveImageSrc = "repeat_active.png";
        private const string NoTrackImageSrc = "notrack.jpg";

        private readonly PlayerService playerService;
        private readonly Page page;

        public event PropertyChangedEventHandler PropertyChanged;

        public PlayerViewModel(PlayerService playerService, Page page)
        {
            this.page = page;
            this.playerService = playerService;
            PlayPauseCommand = new Command(PlayPauseClicked);
            NextCommand = new Command(NextClicked);
            PrevCommand = new Command(PrevClicked);
            RepeatCommand = new Command(RepeatClicked);
            PlaylistCommand = new Command(PlaylistClicked);
            CrossMediaManager.Current.PositionChanged += PositionChanged;
            CrossMediaManager.Current.MediaItemChanged += MediaItemChanged;
            CrossMediaManager.Current.StateChanged += CurrentOnStateChanged;
            CrossMediaManager.Current.RequestHeaders.Add("Authorization", $"Bearer {App.Token}");
            Volume = CrossMediaManager.Current.Volume.CurrentVolume;
            Artist = "Artist";
            Title = "Title";
            PositionTimeSpan = "00:00";
            DurationTimeSpan = "00:00";
            Image = NoTrackImageSrc;
            PlayImage = PlayImageSrc;
            RepeatImage = RepeatImageSrc;
        }

        public int Volume
        {
            get => playerService.Volume;
            set
            {
                playerService.Volume = value;
                OnPropertyChanged();
            }
        }

        public string PositionTimeSpan
        {
            get => playerService.PositionTimeSpan;
            set
            {
                playerService.PositionTimeSpan = value;
                OnPropertyChanged();
            }
        }

        public string DurationTimeSpan
        {
            get => playerService.DurationTimeSpan;
            set
            {
                playerService.DurationTimeSpan = value;
                OnPropertyChanged();
            }
        }

        public ImageSource Image
        {
            get => playerService.Image;
            set
            {
                playerService.Image = value;
                OnPropertyChanged();
            }
        }

        public ImageSource PlayImage
        {
            get => playerService.PlayImage;
            set
            {
                playerService.PlayImage = value;
                OnPropertyChanged();
            }
        }

        public ImageSource RepeatImage
        {
            get => playerService.RepeatImage;
            set
            {
                playerService.RepeatImage = value;
                OnPropertyChanged();
            }
        }

        public int Duration
        {
            get => playerService.Duration;
            set
            {
                playerService.Duration = value;
                OnPropertyChanged();
            }
        }

        public int Position
        {
            get => playerService.Position;
            set
            {
                playerService.Position = value;
                OnPropertyChanged();
            }
        }

        public string Artist
        {
            get => playerService.Artist;
            set
            {
                playerService.Artist = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => playerService.Title;
            set
            {
                playerService.Title = value;
                OnPropertyChanged();
            }
        }

        public Command PlayPauseCommand { get; set; }
        public Command NextCommand { get; set; }
        public Command PrevCommand { get; set; }
        public Command RepeatCommand { get; set; }
        public Command PlaylistCommand { get; set; }

        public void OnVolumeChangedEventHandler(object obj, ValueEventArgs e)
        {
            CrossMediaManager.Current.Volume.CurrentVolume = (int) e.Value;
        }

        public async void OnPositionChangedEventHandler(object obj, ValueEventArgs e)
        {
            var diff = e.Value - playerService.PrevPosition;
            if (diff > 1 || diff < 0)
            {
                await CrossMediaManager.Current.SeekTo(TimeSpan.FromSeconds(e.Value));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void PlayPauseClicked(object obj)
        {
            var mediaItem = await playerService.PlayPause();
            if (mediaItem != null)
            {
                await UpdateTrackData(mediaItem);
            }
        }

        private async void NextClicked(object obj)
        {
            var mediaItem = await playerService.Next();
            await UpdateTrackData(mediaItem);
        }

        private async void PrevClicked(object obj)
        {
            var mediaItem = await playerService.Prev();
            await UpdateTrackData(mediaItem);
        }

        private void RepeatClicked(object obj)
        {
            if (CrossMediaManager.Current.RepeatMode == RepeatMode.One)
            {
                CrossMediaManager.Current.RepeatMode = RepeatMode.Off;
                RepeatImage = RepeatImageSrc;
            }
            else
            {
                CrossMediaManager.Current.RepeatMode = RepeatMode.One;
                RepeatImage = RepeatActiveImageSrc;
            }
        }

        private async void PlaylistClicked(object obj)
        {
            var songsPage = new SongsPage();
            await page.Navigation.PushAsync(songsPage);
        }

        private async Task UpdateTrackData(IMediaItem track)
        {
            Artist = track.Artist;
            Title = track.Title;
            var imageBitmap = (Bitmap)await CrossMediaManager.Current.Extractor.GetMediaImage(track);
            var imgSrc = ImageSource.FromStream(() =>
            {
                var ms = new MemoryStream();
                imageBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                ms.Seek(0L, SeekOrigin.Begin);
                return ms;
            });

            Image = imgSrc;
            Duration = (int)track.Duration.TotalSeconds;
            DurationTimeSpan = track.Duration.ToString(@"mm\:ss");
        }

        private void PositionChanged(object obj, PositionChangedEventArgs args)
        {
            playerService.PrevPosition = playerService.Position;
            PositionTimeSpan = args.Position.ToString(@"mm\:ss");
            Position = (int)args.Position.TotalSeconds;
        }

        private void CurrentOnStateChanged(object sender, StateChangedEventArgs e)
        {
            if (e.State == MediaPlayerState.Paused)
            {
                PlayImage = PlayImageSrc;
            }
            else if (e.State == MediaPlayerState.Playing)
            {
                PlayImage = PauseImageSrc;
            }
        }

        private async void MediaItemChanged(object obj, MediaItemEventArgs args)
        {
            await UpdateTrackData(args.MediaItem);
        }
    }
}
