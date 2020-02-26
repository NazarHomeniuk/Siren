using Ninject;
using Siren.Contracts.Services;
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
            var profileService = App.Kernel.Get<IProfileService>();
            InitializeComponent();
            BindingContext = new ContactProfileViewModel(profileService, this);
        }


    }
}