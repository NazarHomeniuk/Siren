using Siren.Contracts.Models.Chat;
using Siren.Contracts.Services;
using Siren.ViewModels.Chat;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Siren.Views.Chat
{
    /// <summary>
    /// Page to show chat message list
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatMessagePage
    {
        private readonly ChatMessageViewModel viewModel;
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessagePage" /> class.
        /// </summary>
        public ChatMessagePage(Conversation conversation)
        {
            InitializeComponent();
            var chatService = App.Kernel.GetService<IChatService>();
            viewModel = new ChatMessageViewModel(chatService, this, conversation);
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.Connect();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await viewModel.Disconnect();
        }
    }
}