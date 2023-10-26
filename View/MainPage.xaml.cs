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
        var vm = (MainViewModel)this.BindingContext;
        if (vm.AddCommand.CanExecute(e.Location))
            vm.AddCommand.ExecuteAsync(e.Location);
        
    }

    private void RefreshButton_Clicked(object sender, EventArgs e)
    {
        var vm = (MainViewModel)this.BindingContext;
        vm.RefreshCommand.ExecuteAsync();
    }

    private void MoveMapLocation()
    {
        //Function to avoid boilerplate code
        MapSpan mapSpan = new MapSpan(MainViewModel.GetLocation(), 0.01, 0.01);
        mainmap.MoveToRegion(mapSpan);
    }

    private void PopulateMapWithPins()
    {
        var vm = (MainViewModel)this.BindingContext;
        foreach (var restroom in vm.Restroom)
        {
            Pin pin = new Pin
            {
                Label = restroom.Name,
                Address = restroom.Address,
                Type = PinType.Generic,
                Location = new Location(restroom.Latitude, restroom.Longitude)
            };

            mainmap.Pins.Add(pin);
        }
    }
}


