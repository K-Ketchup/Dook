namespace Dook;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Diagnostics;
using Map = Microsoft.Maui.Controls.Maps.Map;
using Dook.ViewModel;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
        InitializeComponent();
        Location location = new(33.7705078125, -118.3725310827384);
        MapSpan mapspan = new(location, 0.01, 0.01);
        mainmap.MoveToRegion(mapspan);
    }


    private void Button_Clicked(object sender, EventArgs e)
    {
        Location location = new(33.7705078125, -118.3725310827384);
        MapSpan mapspan = new(location, 0.01, 0.01);
        mainmap.MoveToRegion(mapspan);
    }
}


