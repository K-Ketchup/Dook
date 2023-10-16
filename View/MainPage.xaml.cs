namespace Dook;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Diagnostics;
using Map = Microsoft.Maui.Controls.Maps.Map;
using Dook.ViewModel;
using Dook.Model;

public partial class MainPage : ContentPage
{
    private Location currentLocation;
	public MainPage()
	{
        InitializeComponent();
        MoveMapLocation();
    }

    private void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        for(int i = 0; i < MainViewModel.Restroom.Count; i++)
        {
            Restroom rt = MainViewModel.Restroom[i];

            Pin pin = new Pin
            {
                Label = "Test Pin",
                Address = "Kenshos House",
                Type = PinType.Generic,
                Location = new Location(e.Location.Latitude, e.Location.Longitude)
            };

            mainmap.Pins.Add(pin);

        }
    }

    private void GoToLocation_Button(object sender, EventArgs e)
    {
        MoveMapLocation();
    }

    private void Refresh_Button(object sender, EventArgs e)
    {
        for (int i = 0; i < MainViewModel.Restroom.Count; i++)
        {
            Restroom rt = MainViewModel.Restroom[i];

            Pin pin = new Pin
            {
                Label = rt.Name,
                Address = rt.Address,
                Type = PinType.Generic,
                //Make it so restroom type objects have a latitutude and longitude.
                Location = new Location(rt.PinLocation)
            };

            mainmap.Pins.Add(pin);

        }
    }

    private void MoveMapLocation()
    {
        //Function to avoid boilerplate code
        currentLocation = MainViewModel.GetLocation();
        MapSpan mapSpan = new MapSpan(currentLocation, 0.01, 0.01);
        mainmap.MoveToRegion(mapSpan);
    }
}


