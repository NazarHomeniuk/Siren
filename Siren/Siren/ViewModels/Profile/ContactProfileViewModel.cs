using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Siren.Contracts.Models.Profile;
using Siren.Contracts.Services;
using Siren.Models;
using Siren.Services;
using Siren.Views.Profile;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.Border;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Siren.ViewModels.Profile
{
    /// <summary>
    /// ViewModel for Individual profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ContactProfileViewModel : INotifyPropertyChanged
    {
        private readonly IUserService userService;
        private readonly IProfileService profileService;
        private readonly IAudioService audioService;
        private readonly PlayerService playerService;
        private readonly ContactProfilePage page;

        #region Field

        private List<Track> tracks;
        private ContactProfile profileInfo;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ContactProfileViewModel" /> class.
        /// </summary>
        public ContactProfileViewModel(IUserService userService, IProfileService profileService,
            IAudioService audioService, PlayerService playerService, ContactProfilePage page)
        {
            page.Appearing += PageOnAppearing;
            this.userService = userService;
            this.profileService = profileService;
            this.audioService = audioService;
            this.playerService = playerService;
            this.page = page;
            ProfileInfo = new ContactProfile();
            ProfileNameCommand = new Command(ProfileNameClicked);
            EditCommand = new Command(EditButtonClicked);
            ViewAllCommand = new Command(ViewAllButtonClicked);
            SelectionChangedCommand = new Command(SelectionChanged);
            RemoveTrackCommand = new Command(RemoveTrackClicked);
        }

        #endregion

        #region Event
        /// <summary>
        /// The declaration of the property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Property

        public List<Track> Tracks
        {
            get => tracks;
            set
            {
                tracks = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a collection of profile info.
        /// </summary>
        public ContactProfile ProfileInfo
        {
            get
            {
                return profileInfo;
            }

            set
            {
                profileInfo = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Command

        public Command RemoveTrackCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the profile name is clicked.
        /// </summary>
        public Command ProfileNameCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the edit button is clicked.
        /// </summary>
        public Command EditCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the view all button is clicked.
        /// </summary>
        public Command ViewAllCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the media image is clicked.
        /// </summary>
        public Command SelectionChangedCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        public void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Invoked when the profile name is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void ProfileNameClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as SfBorder).BackgroundColor = (Color)retVal;
            await Task.Delay(100);

            Application.Current.Resources.TryGetValue("Gray-White", out var oldVal);
            (obj as SfBorder).BackgroundColor = (Color)oldVal;
        }

        /// <summary>
        /// Invoked when the edit button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void EditButtonClicked(object obj)
        {
            await CrossMedia.Current.Initialize();
            var mediaOptions = new PickMediaOptions
            {
                PhotoSize = PhotoSize.Large
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
            if (selectedImageFile == null)
            {
                return;
            }

            byte[] array;
            using (var stream = new MemoryStream())
            {
                await selectedImageFile.GetStream().CopyToAsync(stream);
                array = stream.ToArray();
            }

            var result = await profileService.UpdateUserPhoto(array);
            if (result)
            {
                await UpdateProfileInformation();
            }
        }

        public async Task UpdateProfileInformation()
        {
            Tracks = (await userService.GetCurrentUserTracks()).ToList();
            var currentUser = await profileService.GetCurrentUserInfo();
            ProfileInfo.Name = currentUser.UserName;
            ProfileInfo.Email = currentUser.Email;
            ProfileInfo.ImagePath = currentUser.ImagePath;
            if (!string.IsNullOrEmpty(currentUser.TrackArtist + currentUser.TrackTitle))
            {
                ProfileInfo.Status = $"{currentUser.TrackArtist} - {currentUser.TrackTitle}";
            }
        }

        private async void RemoveTrackClicked(object obj)
        {
            var trackId = (int) obj;
            await userService.RemoveTrack(trackId);
            await UpdateProfileInformation();
        }

        /// <summary>
        /// Invoked when the view all button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void ViewAllButtonClicked(object obj)
        {
            // Do something
        }

        private async void SelectionChanged(object obj)
        {
            if (obj is ItemSelectionChangedEventArgs args)
            {
                if (args.AddedItems.First() is Track track) await playerService.Play(track.Id);
            }
        }

        private async void PageOnAppearing(object sender, EventArgs e)
        {
            await UpdateProfileInformation();
        }

        #endregion
    }
}