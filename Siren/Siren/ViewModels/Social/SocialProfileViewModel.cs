using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Siren.Contracts.Models.Profile;
using Siren.Contracts.Services;
using Siren.Models.Navigation;
using Siren.Views.Chat;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using ProfileModel = Siren.Models.Profile;

namespace Siren.ViewModels.Social
{
    /// <summary>
    /// ViewModel for social profile pages.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SocialProfileViewModel : BaseViewModel
    {
        private const string FollowText = "FOLLOW";
        private const string UnfollowText = "UNFOLLOW";

        private readonly IUserService userService;
        private readonly IChatService chatService;

        #region Fields

        private readonly string userId;

        private readonly Page page;

        private ObservableCollection<ProfileModel> interests;

        private ObservableCollection<Song> songsPageList;

        private UserProfileInfo userProfileInfo;

        private string headerImagePath;

        private string followButtonText;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SocialProfileViewModel" /> class.
        /// </summary>
        public SocialProfileViewModel(string userId, IUserService userService, IChatService chatService, Page page)
        {
            this.page = page;
            this.userId = userId;
            this.userService = userService;
            this.chatService = chatService;
            UpdateProfileInfo();
            FollowCommand = new Command(FollowClicked);
            AddConnectionCommand = new Command(AddConnectionClicked);
            ImageTapCommand = new Command(ImageClicked);
            ProfileSelectedCommand = new Command(ProfileClicked);
        }

        #endregion

        #region Commands

        private Command<object> addCommand;

        private Command<object> chatCommand;

        /// <summary>
        /// Gets or sets the command that is executed when the Follow button is clicked.
        /// </summary>
        public ICommand FollowCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the AddConnection button is clicked.
        /// </summary>
        public ICommand AddConnectionCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Image is tapped.
        /// </summary>
        public ICommand ImageTapCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the profile item is tapped.
        /// </summary>
        public ICommand ProfileSelectedCommand { get; set; }

        public Command<object> AddCommand => addCommand ?? (addCommand = new Command<object>(AddTrackClicked));

        public Command<object> ChatCommand => chatCommand ?? (chatCommand = new Command<object>(ChatClicked));

        #endregion

        #region Properties

        public UserProfileInfo UserProfileInfo
        {
            get => userProfileInfo;
            set
            {
                userProfileInfo = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the interests collection.
        /// </summary>
        public ObservableCollection<ProfileModel> Interests
        {
            get
            {
                return interests;
            }

            set
            {
                interests = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Song> SongsPageList
        {
            get => songsPageList;
            set
            {
                songsPageList = value;
                OnPropertyChanged();
            }
        }

        public string FollowButtonText
        {
            get => followButtonText;
            set
            {
                followButtonText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the header image path.
        /// </summary>
        public string HeaderImagePath
        {
            get => headerImagePath;
            set
            {
                headerImagePath = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Follow button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void FollowClicked(object obj)
        {
            if (UserProfileInfo.IsFollowed)
            {
                var unfollowResult = await userService.Unfollow(userId);
                FollowButtonText = unfollowResult ? FollowText : UnfollowText;
            }
            else
            {
                var followResult = await userService.Follow(userId);
                FollowButtonText = followResult ? UnfollowText : FollowText;
            }

            UpdateProfileInfo();
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

        /// <summary>
        /// Invoked when the AddConnection button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void AddConnectionClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Image is tapped.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ImageClicked(object obj)
        {
            // Do something
        }

        private async void ChatClicked(object obj)
        {
            var conversation = await chatService.StartConversation(userId);
            var chatPage = new ChatMessagePage(conversation);
            await page.Navigation.PushAsync(chatPage);
        }

        /// <summary>
        /// Invoked when the profile is tapped.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ProfileClicked(object obj)
        {
            // Do something
        }

        private async void UpdateProfileInfo()
        {
            UserProfileInfo = await userService.GetUserProfileInfo(userId);
            var songs = await userService.GetUserTracks(userId);
            SongsPageList = new ObservableCollection<Song>(songs.Select(s => new Song
            {
                Composer = s.Artist,
                SongName = s.Title,
                Id = s.Id
            }));
            HeaderImagePath = "";
            FollowButtonText = UserProfileInfo.IsFollowed ? UnfollowText : FollowText;

            Interests = new ObservableCollection<ProfileModel>
            {
                 new ProfileModel { Name = "Food", ImagePath = App.BaseImageUrl + "Recipe12.png" },
                 new ProfileModel { Name = "Travel", ImagePath = App.BaseImageUrl + "Album5.png" },
                 new ProfileModel { Name = "Music", ImagePath = App.BaseImageUrl + "ArticleImage7.jpg" },
                 new ProfileModel { Name = "Bags", ImagePath = App.BaseImageUrl + "Accessories.png" },
                 new ProfileModel { Name = "Market", ImagePath = App.BaseImageUrl + "PersonalCare.png" },
                 new ProfileModel { Name = "Food", ImagePath = App.BaseImageUrl + "Recipe12.png" },
                 new ProfileModel { Name = "Travel", ImagePath = App.BaseImageUrl + "Album5.png" },
                 new ProfileModel { Name = "Music", ImagePath = App.BaseImageUrl + "ArticleImage7.jpg" },
                 new ProfileModel { Name = "Bags", ImagePath = App.BaseImageUrl + "Accessories.png" },
                 new ProfileModel { Name = "Market", ImagePath = App.BaseImageUrl + "PersonalCare.png" }
            };
        }

        #endregion
    }
}
