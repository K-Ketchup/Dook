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
using CommunityToolkit.Maui.Core.Extensions;

namespace Dook.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Restroom> Restroom { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Location> AddCommand { get; }
        public AsyncCommand<Restroom> RemoveCommand { get; }

        public MainViewModel()
        {
            Title = "Map Controller";

            //MainThread.BeginInvokeOnMainThread(async () =>
            //{
            //    var restroom1 = await RestroomService.GetPin().Result;
            //    Restroom = restroom1 as ObservableRangeCollection<Restroom>;
            //});

            //Restroom = new ObservableRangeCollection<Restroom>();

            MainThread.BeginInvokeOnMainThread(async () => Restroom = (ObservableRangeCollection<Restroom>)(await RestroomService.GetPin()).ToObservableCollection());

            AddCommand = new AsyncCommand<Location>(Add);
            RemoveCommand = new AsyncCommand<Restroom>(Remove);
            RefreshCommand = new AsyncCommand(Refresh);
        }

        async Task Add(Location pinlocation)
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Location Name", "Name of Location");
           // var address = "Latitude: {pinlocation.Latitude}, Longitude: {pinlocation.Longitude}, Altitude: {location.Altitude}";
            var address = "bruh chungus ave";
            var username = await App.Current.MainPage.DisplayPromptAsync("Username", "Username of Toilet Adder");
            Location location = pinlocation;
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
