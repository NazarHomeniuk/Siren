using Siren.Contracts.Services;
using Siren.ViewModels.Map;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Siren.Views.Map
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            var mapService = App.Kernel.GetService<IMapService>();
            BindingContext = new MapViewModel(this, mapService);
        }
    }
}