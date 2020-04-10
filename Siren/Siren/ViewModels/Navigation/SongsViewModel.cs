using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Siren.Annotations;
using Siren.Contracts.Services;
using Siren.Models.Navigation;
using Siren.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace Siren.ViewModels.Navigation
{
    /// <summary>
    /// ViewModel for the Navigation list with cards page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class SongsViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly Page page;

        private readonly PlayerService playerService;

        private readonly IAudioService audioService;

        private readonly IUserService userService;

        private Command<object> backButtonCommand;

        private Command<object> itemTappedCommand;

        private Command<object> addCommand;

        private ObservableCollection<Song> songsPageList;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SongsViewModel"/> class.
        /// </summary>
        public SongsViewModel(Page page, PlayerService playerService, IAudioService audioService, IUserService userService)
        {
            this.page = page;
            this.playerService = playerService;
            this.audioService = audioService;
            this.userService = userService;
            Init();
        }

        #endregion

        #region Properties

        public Command<object> AddCommand => addCommand ?? (addCommand = new Command<object>(AddTrackClicked));

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> ItemTappedCommand => this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> BackButtonCommand => this.backButtonCommand ?? (this.backButtonCommand = new Command<object>(this.BackButtonClicked));

        /// <summary>
        /// Gets or sets a collection of values to be displayed in the songs list page.
        /// </summary>
        [DataMember(Name = "songsPageList")]
        public ObservableCollection<Song> SongsPageList
        {
            get => songsPageList;
            set
            {
                songsPageList = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected from the navigation list.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private async void NavigateToNextPage(object selectedItem)
        {
            if (selectedItem is ItemTappedEventArgs args)
            {
                if (args.ItemData is Song song) await playerService.Play(song.Id);
            }
        }

        /// <summary>
        /// Invoked when the back button is clicked.
        /// </summary>
        /// <param name="obj">The sender.</param>
        private async void BackButtonClicked(object obj)
        {
            await page.Navigation.PopAsync();
        }

        private async void AddTrackClicked(object obj)
        {
            var trackId = (int)obj;
            var result = await userService.AddTrack(trackId);
            if (result)
            {
                await page.DisplayAlert("Info", "Track was added to your library", "OK");
            }
            else
            {
                await page.DisplayAlert("Info", "You've already added this track", "OK");
            }
        }

        private async void Init()
        {
            var tracks = await audioService.GetAllTracks();
            SongsPageList = new ObservableCollection<Song>(tracks.Select(t => new Song
            {
                Id = t.Id,
                Composer = t.Artist,
                SongName = t.Title,
                SongImage = "notrack.jpg"
            }));
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}