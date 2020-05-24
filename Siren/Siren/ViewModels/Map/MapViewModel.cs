using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Siren.Annotations;
using Siren.Contracts.Services;
using Siren.Views.Map;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Internals;

namespace Siren.ViewModels.Map
{
    [Preserve(AllMembers = true)]
    public class MapViewModel : INotifyPropertyChanged
    {
        private readonly MapPage page;
        private readonly IGeolocator geoLocator;
        private readonly IMapService mapService;
        private readonly Xamarin.Forms.GoogleMaps.Map map;

        public MapViewModel(MapPage page, IMapService mapService)
        {
            this.mapService = mapService;
            geoLocator = CrossGeolocator.Current;
            geoLocator.PositionChanged += GeoLocatorOnPositionChanged;
            page.Appearing += OnAppearing;
            page.Disappearing += OnDisappearing;
            map = new Xamarin.Forms.GoogleMaps.Map
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
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            page.Content = stack;
        }

        private async void GeoLocatorOnPositionChanged(object sender, PositionEventArgs e)
        {
            await mapService.UpdateUserPosition(e.Position.Longitude, e.Position.Latitude);
        }

        private async void OnDisappearing(object sender, EventArgs e)
        {
            await geoLocator.StopListeningAsync();
        }

        private async void OnAppearing(object sender, EventArgs e)
        {
            await geoLocator.StartListeningAsync(TimeSpan.FromSeconds(1), 2);
            await UpdateMap();
        }

        private async Task UpdateMap()
        {
            var mapUsers = await mapService.GetMapUsers();
            map.Pins.Clear();
            foreach (var userMapInfo in mapUsers)
            {
                var pin = new Pin
                {
                    Label = userMapInfo.UserName,
                    Position = new Xamarin.Forms.GoogleMaps.Position(userMapInfo.Latitude, userMapInfo.Longitude),
                    Type = PinType.Generic,
                    Icon = BitmapDescriptorFactory.FromView(new PinView(userMapInfo.UserName, userMapInfo.TrackInfo,
                        userMapInfo.UserId, App.BaseImageUrl + userMapInfo.UserId))
                };
                map.Pins.Add(pin);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
