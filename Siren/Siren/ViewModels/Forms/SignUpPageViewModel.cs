using Siren.Contracts.Models.Authorization;
using Siren.Contracts.Services;
using Siren.Views.Forms;
using Siren.Views.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Siren.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        private readonly IAuthorizationService authorizationService;
        private readonly SignUpPage page;

        #region Fields

        private string name;

        private string password;

        private string confirmPassword;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel(SignUpPage page, IAuthorizationService authorizationService)
        {
            this.page = page;
            this.authorizationService = authorizationService;
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.name = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password confirmation from users in the Sign Up page.
        /// </summary>
        public string ConfirmPassword
        {
            get
            {
                return this.confirmPassword;
            }

            set
            {
                if (this.confirmPassword == value)
                {
                    return;
                }

                this.confirmPassword = value;
                this.OnPropertyChanged();
            }
        }

        public INavigation Navigation { get; set; }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            var signUpRequest = new SignUpRequest
            {
                Email = Email,
                Password = Password,
                ConfirmPassword = ConfirmPassword,
                Name = Name
            };
            var result = await authorizationService.SignUp(signUpRequest);
            if (result.IsSuccess)
            {
                App.IsUserLoggedId = true;
                App.Token = result.Token;
                Application.Current.MainPage = new NavigationPage(new BottomNavigationPage());
            }
            else
            {
                await page.DisplayAlert("Error", result.ErrorMessage, "Ok");
            }
        }

        #endregion
    }
}