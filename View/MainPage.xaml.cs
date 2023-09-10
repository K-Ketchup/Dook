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

        MapSpan mapSpan = new(MainViewModel.CurrentLocation, 0.1, 0.1);
        mainmap.MoveToRegion(mapSpan);

    }
}


