namespace Dook;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Diagnostics;
using Map = Microsoft.Maui.Controls.Maps.Map;
using Dook.ViewModel;
using Dook.Model;
using Dook.Services;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
        InitializeComponent();
        MoveMapLocation();
        PopulateMap();
    }

    private void GoToLocation_Button(object sender, EventArgs e)
    {
        MoveMapLocation();
    }

    private async void RefreshButton_Clicked(object sender, EventArgs e)
    {
        await PopulateMap();
    }

    private async void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        var vm = (MainViewModel)this.BindingContext;
        if (vm.AddCommand.CanExecute(e.Location))
            await vm.AddCommand.ExecuteAsync(e.Location);
        await PopulateMap();
    }

    private void MoveMapLocation()
    {
        MapSpan mapSpan = new MapSpan(MainViewModel.GetLocation(), 0.01, 0.01);
        mainmap.MoveToRegion(mapSpan);
    }

    private async Task PopulateMap()
    {
        var restroomList = await RestroomService.GetPinAsync();
        foreach (var restroom in restroomList)
        {
            Pin pin = new Pin
            {
                Label = restroom.Name,
                Address = restroom.Address,
                Type = PinType.Generic,
                Location = new Location(restroom.Latitude, restroom.Longitude)
            };
            pin.MarkerClicked += async (s, args) =>
            {
                args.HideInfoWindow = true;
                var vm = (MainViewModel)this.BindingContext;
                mainmap.Pins.Remove(pin);
                if(vm.RemoveCommand.CanExecute(restroom))
                    await vm.RemoveCommand.ExecuteAsync(restroom);
                await PopulateMap();
            };

            mainmap.Pins.Add(pin);
        }
    }
}


