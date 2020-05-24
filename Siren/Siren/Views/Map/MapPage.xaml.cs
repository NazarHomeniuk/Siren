using System;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using MapType = Xamarin.Forms.GoogleMaps.MapType;
using Position = Xamarin.Forms.GoogleMaps.Position;

namespace Siren.Views.Map
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private readonly IGeolocator geolocator;

        public MapPage()
        {
            InitializeComponent();
            var pin = new Pin
            {
                Label = "Test pin",
                Position = new Position(49.000377, 25.854027),
                Type = PinType.Generic,
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
            geolocator = CrossGeolocator.Current;
            geolocator.PositionChanged += GeolocatorOnPositionChanged;
            map.Pins.Add(pin);
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;
            Appearing += OnAppearing;
            Disappearing += OnDisappearing;
        }

        private async void OnDisappearing(object sender, EventArgs e)
        {
            await geolocator.StopListeningAsync();
        }

        private async void OnAppearing(object sender, EventArgs e)
        {
            await geolocator.StartListeningAsync(TimeSpan.FromSeconds(1), 2);
        }

        private void GeolocatorOnPositionChanged(object sender, PositionEventArgs e)
        {
            
        }
    }
}