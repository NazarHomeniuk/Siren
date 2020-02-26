using Ninject;
using Xamarin.Essentials;
using Xamarin.Forms;
using Siren.Services;
using Siren.Views.Forms;
using Siren.Views.Navigation;

namespace Siren
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:40001" : "http://localhost:40001";

        public static string ApiUrl = "http://10.0.2.2:40001/api/";
        public static bool UseMockDataStore = true;
        public static bool IsUserLoggedId { get; set; }
        public static string Token { get; set; }
        public static StandardKernel Kernel { get; private set; }

        public App()
        {
            InitializeComponent();
            Kernel = new StandardKernel(new CommonModule());

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();
            MainPage = IsUserLoggedId
                ? new NavigationPage(new BottomNavigationPage())
                : new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
