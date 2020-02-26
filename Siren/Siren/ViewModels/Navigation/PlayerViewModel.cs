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
using Siren.Contracts.Services;
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

        private readonly IAudioService audioService;

        private ImageSource image;
        private ImageSource playImage;
        private ImageSource repeatImage;
        private string artist;
        private string title;
        private bool isLoaded;
        private int position;
        private int prevPosition;
        private int duration;
        private string positionTimeSpan;
        private string durationTimeSpan;
        private int volume;

        public event PropertyChangedEventHandler PropertyChanged;

        public PlayerViewModel(IAudioService audioService)
        {
            this.audioService = audioService;
            PlayPauseCommand = new Command(PlayPauseClicked);
            NextCommand = new Command(NextClicked);
            PrevCommand = new Command(PrevClicked);
            RepeatCommand = new Command(RepeatClicked);
            CrossMediaManager.Current.PositionChanged += PositionChanged;
            CrossMediaManager.Current.MediaItemChanged += MediaItemChanged;
            CrossMediaManager.Current.StateChanged += CurrentOnStateChanged;
            Volume = CrossMediaManager.Current.Volume.CurrentVolume;
            Artist = "Artist";
            Title = "Title";
            PositionTimeSpan = "00:00";
            DurationTimeSpan = "00:00";
            Image = "notrack.jpg";
            PlayImage = PlayImageSrc;
            RepeatImage = RepeatImageSrc;
        }

        public int Volume
        {
            get => volume;
            set
            {
                volume = value;
                OnPropertyChanged();
            }
        }

        public string PositionTimeSpan
        {
            get => positionTimeSpan;
            set
            {
                positionTimeSpan = value;
                OnPropertyChanged();
            }
        }

        public string DurationTimeSpan
        {
            get => durationTimeSpan;
            set
            {
                durationTimeSpan = value;
                OnPropertyChanged();
            }
        }

        public ImageSource Image
        {
            get => image;
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }

        public ImageSource PlayImage
        {
            get => playImage;
            set
            {
                playImage = value;
                OnPropertyChanged();
            }
        }

        public ImageSource RepeatImage
        {
            get => repeatImage;
            set
            {
                repeatImage = value;
                OnPropertyChanged();
            }
        }

        public int Duration
        {
            get => duration;
            set
            {
                duration = value;
                OnPropertyChanged();
            }
        }

        public int Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        public string Artist
        {
            get => artist;
            set
            {
                artist = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public Command PlayPauseCommand { get; set; }
        public Command NextCommand { get; set; }
        public Command PrevCommand { get; set; }
        public Command RepeatCommand { get; set; }

        public void OnVolumeChangedEventHandler(object obj, ValueEventArgs e)
        {
            CrossMediaManager.Current.Volume.CurrentVolume = (int) e.Value;
        }

        public async void OnPositionChangedEventHandler(object obj, ValueEventArgs e)
        {
            var diff = e.Value - prevPosition;
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
            if (!isLoaded)
            {
                var track = await audioService.PlayAll();
                await UpdateTrackData(track);
                isLoaded = true;
                PlayImage = PauseImageSrc;
                return;
            }

            if (CrossMediaManager.Current.IsPlaying())
            {
                PlayImage = PlayImageSrc;
                await CrossMediaManager.Current.Pause();
            }
            else
            {
                PlayImage = PauseImageSrc;
                await CrossMediaManager.Current.Play();
            }
        }

        private async void NextClicked(object obj)
        {
            await CrossMediaManager.Current.PlayNext();
        }

        private async void PrevClicked(object obj)
        {
            await CrossMediaManager.Current.PlayPrevious();
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
            prevPosition = position;
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
