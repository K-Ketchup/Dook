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

    private async void GoToLocationButton_Clicked(object sender, EventArgs e)
    {
        await MoveMapUserAsync();
    }

    private async void RefreshButton_Clicked(object sender, EventArgs e)
    {
        await PopulateMapAsync();
    }

    private async void SearchButton_Clicked(object sender, EventArgs e)
    {
        Location location1 = InternetMainViewModel.GetLocation();
        Location closestLocation = null;
        var restroomList = await InternetRestroomService.GetPinAsync();
        //Apparently this is the farthest distance on earth something can be from something else in miles.
        //Will update if theres restrooms on the moon.
        double smallestDistMiles = 24901.451;

        foreach (var restroom in restroomList)
        {
            Location location2 = new Location(restroom.Latitude, restroom.Longitude);
            Distance distance = Distance.BetweenPositions(location1, location2);
            if(distance.Miles < smallestDistMiles)
            {
                smallestDistMiles = distance.Miles;
                closestLocation = location2;
            }
        }

        MapSpan mapSpan = new MapSpan(closestLocation, 0.01, 0.01);
        mainmap.MoveToRegion(mapSpan);
        await Task.Delay(2000);

        if (await DisplayAlert("Directions","Would you like to open directions to this restroom?", "Yes", "No"))
        {
            if (DeviceInfo.Current.Platform == DevicePlatform.iOS || DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst)
            {
                // https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                await Launcher.OpenAsync("http://maps.apple.com/?daddr=" + closestLocation.Latitude + "," + closestLocation.Longitude);

                //?ll=" + closestLocation.Latitude + "," + closestLocation.Longitude
            }
            else if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            {
                // opens the 'task chooser' so the user can pick Maps, Chrome or other mapping app
                await Launcher.OpenAsync("http://maps.google.com/?daddr=" + closestLocation.Latitude + "," + closestLocation.Longitude);
            }
            else
            {
                //Windows cant run map api. 
                await DisplayAlert("Error", "Platform not supported", "OK");
            }
        }
    }

    private async void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        var vm = (InternetMainViewModel)this.BindingContext;
        if (vm.AddCommand.CanExecute(e.Location))
            await vm.AddCommand.ExecuteAsync(e.Location);
        await PopulateMapAsync();
    }

    private async Task MoveMapUserAsync()
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
        MoveMapUserAsync();
        PopulateMapAsync();
    }
}


