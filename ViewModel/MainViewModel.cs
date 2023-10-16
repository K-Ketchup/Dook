using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
using Dook.Model;
using Dook.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace Dook.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        public static ObservableRangeCollection<Restroom> Restroom { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Restroom> RemoveCommand { get; }

        public MainViewModel()
        {
            Title = "Map Controller";

            Restroom = new ObservableRangeCollection<Restroom>();

            AsyncCommand<Location> addCommand = new AsyncCommand<Location>(Add);
            RemoveCommand = new AsyncCommand<Restroom>(Remove);
            RefreshCommand = new AsyncCommand(Refresh);
        }

        async Task Add(Location location)
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Location Name", "Name of Location");
            var address = "Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
            var username = await App.Current.MainPage.DisplayPromptAsync("Username", "Username of Toilet Adder");
            await RestroomService.AddPin(name, address, username, location);
            await Refresh();
        }
        
        async Task Remove(Restroom restroom)
        {
            await RestroomService.RemovePin(restroom.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Restroom.Clear();
            var restrooms = await RestroomService.GetPin();
            Restroom.AddRange(restrooms);
            IsBusy = false;
        }

        public static Location GetLocation()
        {
            try
            {
                Location location = new ();
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
