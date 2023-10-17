namespace Dook;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Diagnostics;
using Map = Microsoft.Maui.Controls.Maps.Map;
using Dook.ViewModel;
using Dook.Model;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
        InitializeComponent();
        MoveMapLocation();
    }

    private void GoToLocation_Button(object sender, EventArgs e)
    {
        MoveMapLocation();
    }

    private void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        var viewModel = new MainViewModel();

        if(viewModel.AddCommand.CanExecute(e.Location)) 
        {
            viewModel.AddCommand.ExecuteAsync(e.Location);
        }

        RefreshPins();

    }

    private void RefreshButton_Clicked(object sender, EventArgs e)
    {
        RefreshPins();
    }

    private void MoveMapLocation()
    {
        //Function to avoid boilerplate code
        MapSpan mapSpan = new MapSpan(MainViewModel.GetLocation(), 0.01, 0.01);
        mainmap.MoveToRegion(mapSpan);
    }

    private void RefreshPins()
    {
        for (int i = 0; i < MainViewModel.Restroom.Count; i++)
        {
            Restroom rt = MainViewModel.Restroom[i];

            Pin pin = new Pin
            {
                Label = rt.Name,
                Address = rt.Address,
                Type = PinType.Generic,
                Location = new Location(rt.PinLocation)
            };

            mainmap.Pins.Add(pin);
        }
    }
}


