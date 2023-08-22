namespace Dook;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Diagnostics;
using Map = Microsoft.Maui.Controls.Maps.Map;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
        GetLocation();
        InitializeComponent();
    }

    private async void GetLocation()
    {
        Location location = await Geolocation.GetLocationAsync(new GeolocationRequest
        {
            DesiredAccuracy = GeolocationAccuracy.High,
            Timeout = TimeSpan.FromSeconds(30)
        });
        if(location != null)
        {
            MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
            Map map = new Map(mapSpan);
        }

        Debug.Print("Bruh");
    }
}


