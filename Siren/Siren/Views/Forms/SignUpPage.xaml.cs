using Ninject;
using Siren.Contracts.Services;
using Siren.ViewModels.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Siren.Views.Forms
{
    /// <summary>
    /// Page to sign in with user details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpPage" /> class.
        /// </summary>
        public SignUpPage()
        {
            var authorizationService = App.Kernel.Get<IAuthorizationService>();
            InitializeComponent();
            BindingContext = new SignUpPageViewModel(this, authorizationService);
        }
    }
}