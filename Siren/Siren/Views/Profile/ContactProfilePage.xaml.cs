using Ninject;
using Siren.Contracts.Services;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Siren.Views.Profile
{
    /// <summary>
    /// Page to show Contact profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactProfilePage
    {
        public ContactProfilePage()
        {
            InitializeComponent();
            GetProfileInformation();
            this.ProfileImage.Source =
                "https://scontent.fiev25-1.fna.fbcdn.net/v/t1.0-9/p960x960/82190076_563824704201909_7556769115046674432_o.jpg?_nc_cat=101&_nc_ohc=KkjoMjBq2ZcAX9kOZyD&_nc_ht=scontent.fiev25-1.fna&_nc_tp=6&oh=06e0d7a5ac2cd7ffc495ef299ca034ff&oe=5EC2FCDB";
        }

        private async void GetProfileInformation()
        {
            var profileService = App.Kernel.Get<IProfileService>();
            var currentUser = await profileService.GetCurrentUserInfo();
            this.ProfileName.Text = currentUser.UserName;
            this.EmailLabel.Text = currentUser.Email;
        }
    }
}