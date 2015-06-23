using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Labs;
using System.Threading.Tasks;
using Xamarin.Forms.Labs.Services.Geolocation;
using Geolocator;
using Geolocator.Plugin;

namespace AAJMaps
{
    public class MapPage : ContentPage
    {
        Label lblatlong;

        public MapPage()
        {
            Map map = new Map
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            //var slider = new Slider(1, 18, 1);
            //slider.ValueChanged += (sender, e) =>
            //{
            //    var zoomLevel = e.NewValue; // between 1 and 18
            //    var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            //    map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
            //};

            //var position = new Position(-32.9595, -60.661541); // Latitude, Longitude
            //var pin = new Pin
            //{
            //    Type = PinType.Place,
            //    Position = position,
            //    Label = "custom pin",
            //    Address = "custom detail info"
            //};

            //map.Pins.Add(pin);

            var street = new Button { Text = "Street" };
            lblatlong = new Label { Text = " " };


            street.Clicked += async (sender, e) =>
            {
                var locator = CrossGeolocator.Current;

                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(timeout: 10000);

                lblatlong.Text = "Lat" + position.Latitude.ToString() + "Long" + position.Longitude.ToString();
            };

            var segments = new StackLayout
            {
                Spacing = 30,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { street, lblatlong }
            };

            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            //stack.Children.Add(slider);
            stack.Children.Add(segments);
            Content = stack;

        }        
    }
}
