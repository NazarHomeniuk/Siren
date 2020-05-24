using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using MapType = Xamarin.Forms.GoogleMaps.MapType;

namespace Siren.Views.Map
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            var pin = new Pin
            {
                Label = "Test pin",
                Position = new Position(49.000377, 25.854027),
                Type = PinType.Place,
                Icon = BitmapDescriptorFactory.FromView(new PinView())
            };
            var map = new Xamarin.Forms.GoogleMaps.Map
            {
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand,
                MapType = MapType.Hybrid,
                MyLocationEnabled = true,
                IsTrafficEnabled = true,
                UiSettings =
                {
                    CompassEnabled = true,
                    MyLocationButtonEnabled = true,
                    ZoomControlsEnabled = true
                }
            };
            map.Pins.Add(pin);
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;
        }
    }
}