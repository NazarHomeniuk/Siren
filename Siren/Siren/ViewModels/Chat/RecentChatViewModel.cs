using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.SignalR.Client;
using Siren.Contracts.Services;
using Siren.Models.Chat;
using Siren.Views.Chat;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace Siren.ViewModels.Chat
{
    /// <summary>
    /// View model for recent chat page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class RecentChatViewModel : INotifyPropertyChanged
    {
        private readonly HubConnection hubConnection;
        private readonly RecentChatPage page;
        private readonly IChatService chatService;
        private readonly IProfileService profileService;

        #region Fields

        private ObservableCollection<ChatDetail> chatItems;

        private Command itemSelectedCommand;

        private string profileImage;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RecentChatViewModel" /> class.
        /// </summary>
        public RecentChatViewModel(IChatService chatService, IProfileService profileService, RecentChatPage page)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://10.0.2.2:40001/chat",
                    options => options.Headers.Add("Authorization", $"Bearer {App.Token}"))
                .Build();
            this.page = page;
            this.chatService = chatService;
            this.profileService = profileService;
            MakeVoiceCallCommand = new Command(VoiceCallClicked);
            MakeVideoCallCommand = new Command(VideoCallClicked);
            ShowSettingsCommand = new Command(SettingsClicked);
            MenuCommand = new Command(MenuClicked);
            ProfileImageCommand = new Command(ProfileImageClicked);
            Init();
            hubConnection.On<string, string>("Receive", (s, s1) => Init());
            page.Appearing += PageOnAppearing;
        }
        #endregion

        #region Event

        /// <summary>
        /// The declaration of the property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        public string ProfileImage
        {
            get
            {
                return profileImage;
            }

            set
            {
                profileImage = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the profile items.
        /// </summary>
        public ObservableCollection<ChatDetail> ChatItems
        {
            get => chatItems;

            set
            {
                if (chatItems == value)
                {
                    return;
                }

                chatItems = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Commands
        /// <summary>
        /// Gets or sets the command that is executed when the voice call button is clicked.
        /// </summary>
        public Command MakeVoiceCallCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the video call button is clicked.
        /// </summary>
        public Command MakeVideoCallCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the settings button is clicked.
        /// </summary>
        public Command ShowSettingsCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the menu button is clicked.
        /// </summary>
        public Command MenuCommand { get; set; }

        /// <summary>
        /// Gets the command that is executed when an item is selected.
        /// </summary>
        public Command ItemSelectedCommand
        {
            get { return itemSelectedCommand ?? (itemSelectedCommand = new Command(ItemSelected)); }
        }

        /// <summary>
        /// Gets or sets the command that is executed when the profile image is clicked.
        /// </summary>
        public Command ProfileImageCommand { get; set; }

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
        /// Invoked when an item is selected.
        /// </summary>
        private async void ItemSelected(object selectedItem)
        {
            var item = (ItemTappedEventArgs) selectedItem;
            var detail = (ChatDetail) item.ItemData;
            var conversation = await chatService.GetConversation(detail.Id);
            var chatMessagePage = new ChatMessagePage(conversation);
            await page.Navigation.PushAsync(chatMessagePage);
        }

        /// <summary>
        /// Invoked when the Profile image is clicked.
        /// </summary>
        private void ProfileImageClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the voice call button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void VoiceCallClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the video call button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void VideoCallClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the settings button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SettingsClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the menu button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void MenuClicked(object obj)
        {
            // Do something
        }

        #endregion

        private async void Init()
        {
            var conversations = await chatService.GetConversations();
            ChatItems = new ObservableCollection<ChatDetail>(conversations.Select(c => new ChatDetail
            {
                Id = c.Id,
                Message = c.Messages.Any() ? c.Messages.OrderByDescending(m => m.SentAt).First().Text : "No messages",
                Time = c.Messages.Any() ? c.Messages.OrderByDescending(m => m.SentAt).First().SentAt.ToString(CultureInfo.InvariantCulture) : DateTime.Now.ToString("dd/MM/yyyy"),
                SenderName = c.Participants.First(p => p.UserId != App.UserId).User.UserName,
                ImagePath = App.BaseImageUrl + c.Participants.First(p => p.UserId != App.UserId).User.Id,
                MessageType = "Text",
                NotificationType = "Viewed",
            }));
            var profileInfo = await profileService.GetCurrentUserInfo();
            ProfileImage = profileInfo.ImagePath;
        }

        private void PageOnAppearing(object sender, EventArgs e)
        {
            Init();
        }
    }
}
