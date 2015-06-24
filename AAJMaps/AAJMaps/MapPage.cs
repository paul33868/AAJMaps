using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Labs;
using Xamarin.Forms.Labs.Controls;
using System.Threading.Tasks;
using Xamarin.Forms.Labs.Services.Geolocation; //It's not used rigth now, but it might in the near future
using Geolocator;
using Geolocator.Plugin;

namespace AAJMaps
{
    public class MapPage : ContentPage
    {
        Label lblatlong;

        List<string> lista;

        public MapPage()
        {
            lista = new List<string>();

            Map map = new Map
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var searchAddress = new SearchBar { Placeholder = "Search Address" };
            //var getCoords = new Button { Text = "Street" };
            //lblatlong = new Label { Text = " " };


            //getCoords.Clicked += async (sender, e) =>
            //{
            //    var locator = CrossGeolocator.Current;

            //    locator.DesiredAccuracy = 50;

            //    var position = await locator.GetPositionAsync(timeout: 10000);

            //    lblatlong.Text = "Lat" + position.Latitude.ToString() + "Long" + position.Longitude.ToString();
            //};

            searchAddress.TextChanged += async (e, a) =>
            {
                var addressQuery = searchAddress.Text;
                //searchAddress.Text = "";
                //searchAddress.Focus();
                //searchAddress.Unfocus();

                var positions = (await(new Geocoder()).GetPositionsForAddressAsync(addressQuery)).ToList();
                if (!positions.Any())
                    return;

                foreach (var pos in positions)
                {
                    var locationAddress = (await(new Geocoder()).GetAddressesForPositionAsync(pos));
                    if (locationAddress != null && locationAddress.ToList().Count > 0)
                        lista.Add(locationAddress.ToList()[0]);
                }
            };

            searchAddress.SearchButtonPressed += async (e, a) =>
            {
                lista = new List<string>();

                var addressQuery = searchAddress.Text;
                searchAddress.Text = "";
                searchAddress.Unfocus();

                var positions = (await (new Geocoder()).GetPositionsForAddressAsync(addressQuery)).ToList();
                if (!positions.Any())
                    return;

                var position = positions.First();
                map.MoveToRegion(MapSpan.FromCenterAndRadius(position,
                    Distance.FromMiles(0.1)));
                map.Pins.Add(new Pin
                {
                    Label = addressQuery,
                    Position = position,
                    Address = addressQuery
                });
            };

            var MyList = new ListView
            {
                ItemsSource = lista
            };

            var segments = new StackLayout
            {
                Spacing = 30,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { searchAddress, MyList }
            };

            var stack = new StackLayout { Spacing = 0 };            
            //stack.Children.Add(slider);
            stack.Children.Add(segments);
            stack.Children.Add(map);
            Content = stack;

        }        
    }
}
