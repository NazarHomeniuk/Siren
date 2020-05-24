using Xamarin.Forms;

namespace Siren.Views.Map
{
    public partial class PinView : StackLayout
    {
        private string userName;
        private string trackInfo;
        private string userId;
        private string imagePath;

        public string UserName
        {
            get => userName;
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TrackInfo
        {
            get => trackInfo;
            set
            {
                if (trackInfo != value)
                {
                    trackInfo = value;
                    OnPropertyChanged();
                }
            }
        }

        public string UserId
        {
            get => userId;
            set
            {
                if (userId != value)
                {
                    userId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ImagePath
        {
            get => imagePath;
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public PinView(string userName, string trackInfo, string userId, string imagePath)
        {
            UserName = userName;
            TrackInfo = trackInfo;
            UserId = userId;
            ImagePath = imagePath;
            InitializeComponent();
            BindingContext = this;
        }
    }
}