namespace Dook;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Diagnostics;
using Map = Microsoft.Maui.Controls.Maps.Map;
using Dook.ViewModel;

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
        Pin pin = new Pin
        {
            Label="Test Pin",
            Address="Kenshos House",
            Type=PinType.Generic,
            Location = new Location(e.Location.Latitude, e.Location.Longitude)
        };

        mainmap.Pins.Add(pin);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        MoveMapLocation();
    }

    private void MoveMapLocation()
    {
        currentLocation = MainViewModel.GetLocation();
        MapSpan mapSpan = new MapSpan(currentLocation, 0.01, 0.01);
        mainmap.MoveToRegion(mapSpan);
    }
}


