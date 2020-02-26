using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Siren.Contracts.Services;
using Siren.Models;
using Siren.Views.Profile;
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
        private readonly IProfileService profileService;
        private readonly ContactProfilePage page;

        #region Field

        private ContactProfile profileInfo;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ContactProfileViewModel" /> class.
        /// </summary>
        public ContactProfileViewModel(IProfileService profileService, ContactProfilePage page)
        {
            this.profileService = profileService;
            this.page = page;
            this.ProfileInfo = new ContactProfile();
            this.ProfileNameCommand = new Command(this.ProfileNameClicked);
            this.EditCommand = new Command(this.EditButtonClicked);
            this.ViewAllCommand = new Command(this.ViewAllButtonClicked);
            this.MediaImageCommand = new Command(this.MediaImageClicked);
            Task.Run(async () => { await UpdateProfileInformation(); }).Wait();
        }

        #endregion

        #region Event
        /// <summary>
        /// The declaration of the property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Property

        /// <summary>
        /// Gets or sets a collection of profile info.
        /// </summary>
        public ContactProfile ProfileInfo
        {
            get
            {
                return this.profileInfo;
            }

            set
            {
                this.profileInfo = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Command

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
        public Command MediaImageCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        public void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            var currentUser = await profileService.GetCurrentUserInfo();
            ProfileInfo.Name = currentUser.UserName;
            ProfileInfo.Email = currentUser.Email;
            ProfileInfo.ImagePath = currentUser.ImagePath;
        }

        /// <summary>
        /// Invoked when the view all button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void ViewAllButtonClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the media image is clicked.
        /// </summary>
        private void MediaImageClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}