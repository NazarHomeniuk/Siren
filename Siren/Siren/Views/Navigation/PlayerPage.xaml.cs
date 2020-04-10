using Ninject;
using Siren.Contracts.Services;
using Siren.Services;
using Siren.ViewModels.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Siren.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        public PlayerPage()
        {
            var audioService = App.Kernel.Get<IAudioService>();
            var playerService = App.Kernel.Get<PlayerService>();
            InitializeComponent();
            var viewModel = new PlayerViewModel(playerService, this);
            BindingContext = viewModel;
            RangeSlider.ValueChanging += viewModel.OnPositionChangedEventHandler;
            VolumeSlider.ValueChanging += viewModel.OnVolumeChangedEventHandler;
        }
    }
}