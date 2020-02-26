using Ninject;
using Siren.Contracts.Services;
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
            InitializeComponent();
            var viewModel = new PlayerViewModel(audioService);
            BindingContext = viewModel;
            RangeSlider.ValueChanging += viewModel.OnPositionChangedEventHandler;
            VolumeSlider.ValueChanging += viewModel.OnVolumeChangedEventHandler;
        }
    }
}