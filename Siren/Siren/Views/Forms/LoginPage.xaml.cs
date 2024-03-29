﻿using Ninject;
using Siren.Contracts.Services;
using Siren.ViewModels.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Siren.Views.Forms
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public LoginPage()
        {
            var authorizationService = App.Kernel.Get<IAuthorizationService>();
            InitializeComponent();
            BindingContext = new LoginPageViewModel(this, authorizationService);
        }
    }
}