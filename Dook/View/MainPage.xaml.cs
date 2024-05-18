namespace Dook;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Diagnostics;
using Map = Microsoft.Maui.Controls.Maps.Map;
using Dook.ViewModel;
using Dook.Shared.Models;
using Dook.Services;
using CommunityToolkit.Maui.Behaviors;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
        InitializeComponent();
    }

    private void GoToLocation_Button(object sender, EventArgs e)
    {
        MoveMapLocation();
    }

    private async void RefreshButton_Clicked(object sender, EventArgs e)
    {
        await PopulateMapAsync();
    }

    private async void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        var vm = (InternetMainViewModel)this.BindingContext;
        if (vm.AddCommand.CanExecute(e.Location))
            await vm.AddCommand.ExecuteAsync(e.Location);
        await PopulateMapAsync();
    }

    private void MoveMapLocation()
    {
        MapSpan mapSpan = new MapSpan(InternetMainViewModel.GetLocation(), 0.01, 0.01);
        mainmap.MoveToRegion(mapSpan);
    }

    private async Task PopulateMapAsync()
    {
        var restroomList = await InternetRestroomService.GetPinAsync();
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

                var vm = (InternetMainViewModel)this.BindingContext;
                Debug.Write(restroom.Id);
                if (vm.SelectedCommand.CanExecute(restroom))
                    await vm.SelectedCommand.ExecuteAsync(restroom);

                //mainmap.Pins.Remove(pin);
                //if(vm.RemoveCommand.CanExecute(restroom))
                //    await vm.RemoveCommand.ExecuteAsync(restroom);
                //await PopulateMapAsync();
            };

            mainmap.Pins.Add(pin);
        }
    }

    protected override async void OnAppearing()
    {
        MoveMapLocation();
        PopulateMapAsync();
    }
}


