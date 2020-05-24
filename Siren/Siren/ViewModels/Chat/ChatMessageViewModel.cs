using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Siren.Contracts.Models.Chat;
using Siren.Contracts.Services;
using Siren.Models.Chat;
using Siren.Views.Chat;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Siren.ViewModels.Chat
{
    /// <summary>
    /// ViewModel for chat message page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ChatMessageViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly IChatService chatService;

        private readonly ChatMessagePage page;

        private readonly HubConnection hubConnection;

        private readonly Conversation conversation;

        private bool isBusy;

        private bool isConnected;

        private string profileName;

        private string profileImage;

        private string newMessage;

        private ObservableCollection<ChatMessage> chatMessageInfo = new ObservableCollection<ChatMessage>();

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageViewModel" /> class.
        /// </summary>
        public ChatMessageViewModel(IChatService chatService, ChatMessagePage page, Conversation conversation)
        {
            this.chatService = chatService;
            this.page = page;
            this.conversation = conversation;
            var users = conversation.Participants.Where(u => u.UserId != App.UserId).ToList();
            profileName = string.Join(",", users.Select(u => u.User.UserName));
            profileImage = App.BaseImageUrl + users.First().User.Id;
            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://10.0.2.2:40001/chat",
                    options => options.Headers.Add("Authorization", $"Bearer {App.Token}"))
                .Build();
            IsConnected = false;
            IsBusy = false;
            hubConnection.Closed += async error =>
            {
                IsConnected = false;
                await Task.Delay(5000);
                await Connect();
            };
            InitMessages();
            hubConnection.On<string, string>("Receive", ReceiveMessage);

            MakeVoiceCall = new Command(VoiceCallClicked);
            MakeVideoCall = new Command(VideoCallClicked);
            MenuCommand = new Command(MenuClicked);
            ShowCamera = new Command(CameraClicked);
            SendAttachment = new Command(AttachmentClicked);
            SendCommand = new Command(SendClicked);
            BackCommand = new Command(BackButtonClicked);
            ProfileCommand = new Command(ProfileClicked);

            GenerateMessageInfo();
        }

        #endregion

        #region Event
        /// <summary>
        /// The declaration of the property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public bool IsConnected
        {
            get => isConnected;
            set
            {
                if (isConnected != value)
                {
                    isConnected = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the profile name.
        /// </summary>
        public string ProfileName
        {
            get => profileName;

            set
            {
                profileName = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        public string ProfileImage
        {
            get => profileImage;
            set
            {
                profileImage = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a collection of chat messages.
        /// </summary>
        public ObservableCollection<ChatMessage> ChatMessageInfo
        {
            get => chatMessageInfo;
            set
            {
                chatMessageInfo = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a new message.
        /// </summary>
        public string NewMessage
        {
            get => newMessage;
            set
            {
                newMessage = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the profile name is clicked.
        /// </summary>
        public Command ProfileCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the voice call button is clicked.
        /// </summary>
        public Command MakeVoiceCall { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the video call button is clicked.
        /// </summary>
        public Command MakeVideoCall { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the menu button is clicked.
        /// </summary>
        public Command MenuCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the camera button is clicked.
        /// </summary>
        public Command ShowCamera { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the attachment button is clicked.
        /// </summary>
        public Command SendAttachment { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the send button is clicked.
        /// </summary>
        public Command SendCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the back button is clicked.
        /// </summary>
        public Command BackCommand { get; set; }

        #endregion

        #region Methods

        public async Task Connect()
        {
            if (IsConnected)
                return;
            try
            {
                await hubConnection.StartAsync();
                IsConnected = true;
            }
            catch (Exception ex)
            {
            }
        }

        public async Task Disconnect()
        {
            if (!IsConnected)
                return;

            await hubConnection.StopAsync();
            IsConnected = false;
        }

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        public void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Initializes a collection and add it to the message items.
        /// </summary>
        private void GenerateMessageInfo()
        {

        }

        /// <summary>
        /// Invoked when the voice call button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void VoiceCallClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the video call button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void VideoCallClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the menu button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void MenuClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the camera icon is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void CameraClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the attachment icon is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void AttachmentClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the send button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void SendClicked(object obj)
        {
            if (!string.IsNullOrWhiteSpace(NewMessage))
            {
                await SendMessage();
            }

            NewMessage = null;
        }

        /// <summary>
        /// Invoked when the back button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void BackButtonClicked(object obj)
        {
            await page.Navigation.PopAsync();
        }

        /// <summary>
        /// Invoked when the Profile name is clicked.
        /// </summary>
        private void ProfileClicked(object obj)
        {
            // Do something
        }

        private async Task SendMessage()
        {
            try
            {
                IsBusy = true;
                var message = new Message
                {
                    ConversationId = conversation.Id,
                    IsReceived = false,
                    SentAt = DateTime.Now,
                    SentById = App.UserId,
                    Text = NewMessage
                };
                await chatService.SendMessage(message);
                SendLocalMessage(profileName, NewMessage);
                await hubConnection.InvokeAsync("Send", profileName, newMessage, conversation.Participants.First(p => p.UserId != App.UserId).UserId);
            }
            catch (Exception ex)
            {
                SendLocalMessage(string.Empty, $"Ошибка отправки: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void SendLocalMessage(string user, string message)
        {
            ChatMessageInfo.Insert(ChatMessageInfo.Count, new ChatMessage
            {
                Message = message,
                Time = DateTime.Now,
                IsReceived = false
            });
        }

        private void SendLocalMessageReceived(string user, string message)
        {
            ChatMessageInfo.Insert(ChatMessageInfo.Count, new ChatMessage
            {
                Message = message,
                Time = DateTime.Now,
                IsReceived = true
            });
        }

        private void ReceiveMessage(string user, string message)
        {
            ChatMessageInfo.Insert(ChatMessageInfo.Count, new ChatMessage
            {
                Message = message,
                Time = DateTime.Now,
                IsReceived = true
            });
        }

        private void InitMessages()
        {
            foreach (var message in conversation.Messages)
            {
                if (message.SentById == App.UserId)
                {
                    SendLocalMessage(message.SentBy.UserName, message.Text);
                }
                else
                {
                    SendLocalMessageReceived(message.SentBy.UserName, message.Text);
                }
            }
        }

        #endregion
    }
}
