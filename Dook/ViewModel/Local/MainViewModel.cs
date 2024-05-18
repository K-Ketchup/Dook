using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
using Dook.Shared.Models;
using Dook.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using CommunityToolkit.Maui.Core.Extensions;

namespace Dook.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Restroom> Restroom { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Location> AddCommand { get; }
        public AsyncCommand<Restroom> RemoveCommand { get; }

        public MainViewModel()
        {
            Title = "Map Controller";

            Restroom = new ObservableRangeCollection<Restroom>();

            AddCommand = new AsyncCommand<Location>(AddAsync);
            RemoveCommand = new AsyncCommand<Restroom>(RemoveAsync);
            RefreshCommand = new AsyncCommand(RefreshAsync);
        }

        async Task AddAsync(Location pinlocation)
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Location Name", "Name of Location");
            // var address = "Latitude: {pinlocation.Latitude}, Longitude: {pinlocation.Longitude}, Altitude: {location.Altitude}";
            var address = "test";
            var username = await App.Current.MainPage.DisplayPromptAsync("Username", "Username of Toilet Adder");
            var latitude = pinlocation.Latitude;
            var longitude = pinlocation.Longitude;
            if (name == null || address == null || username == null) { return; }
            await RestroomService.AddPinAsync(name, address, username, latitude, longitude);
            await RefreshAsync();
        }

        async Task RemoveAsync(Restroom restroom)
        {
            await RestroomService.RemovePinAsync(restroom.Id);
            await RefreshAsync();
        }

        async Task RefreshAsync()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Restroom.Clear();
            var restrooms = await RestroomService.GetPinAsync();
            Restroom.AddRange(restrooms);
            IsBusy = false;
        }

        public static Location GetLocation()
        {
            try
            {
                Location location = new();
                location = Geolocation.Default.GetLastKnownLocationAsync().Result;
                if (location != null)
                    return location;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get location: {ex.Message}");
                Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }

            return null;
        }
    }
}
