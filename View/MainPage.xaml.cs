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
        
        MainViewModel mainViewModel = new();
        MapSpan mapSpan = new MapSpan(MainViewModel.GetLocationTest(), 0.01, 0.01);
        Map map = new Map(mapSpan)
        {
            MapType = MapType.Street,
        };
    }


    private void Button_Clicked(object sender, EventArgs e)
    {
        //Location location = MainViewModel.GetLocationTestAsync();
        //MapSpan mapspan = new(location, 0.01, 0.01);
        //mainmap.MoveToRegion(mapspan);
    }
}


