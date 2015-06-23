using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AAJMaps
{
    public class MapPage : ContentPage
    {
        public MapPage()
        {
            var map = new Map(
                MapSpan.FromCenterAndRadius(
                        new Position(-32.9595, -60.661541), Distance.FromMiles(0.3)))
                        {
                            IsShowingUser = true,
                            HeightRequest = 100,
                            WidthRequest = 960,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            //MapType = MapType.Street
                        };

            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;
        }
    }
}
