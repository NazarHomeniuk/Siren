using Ninject;
using Siren.Contracts.Services;
using Siren.Services;
using Siren.ViewModels.Profile;
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
            var userService = App.Kernel.Get<IUserService>();
            var profileService = App.Kernel.Get<IProfileService>();
            var audioService = App.Kernel.Get<IAudioService>();
            var playerService = App.Kernel.Get<PlayerService>();
            InitializeComponent();
            BindingContext =
                new ContactProfileViewModel(userService, profileService, audioService, playerService, this);
        }


    }
}