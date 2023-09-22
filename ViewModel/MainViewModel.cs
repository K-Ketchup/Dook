using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace Dook.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        public double CurrentLocationLatitude { get; set; } = 33.7705078125;
        public double CurrentLocationLongitude { get; set; } = -118.37253108273849;
        public Location CurrentLocation { get; set; }
        public Command GetCurrentLocationCommand { get; }
        public Command MoveMapCommand { get; }
        public MainViewModel()
        {
            Title = "Map Controller";
            GetCurrentLocationCommand = new Command(async () => await GetLocationAsync());
            MoveMapCommand = new Command(async () => await MoveMapAsync(null));
        }

        public async Task GetLocationAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                CurrentLocation = await Geolocation.Default.GetLastKnownLocationAsync();
                if (CurrentLocation != null)
                {
                    CurrentLocationLatitude = CurrentLocation.Latitude;
                    CurrentLocationLongitude = CurrentLocation.Longitude;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get current location: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task MoveMapAsync(MainPage mainPage)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                CurrentLocation = await Geolocation.Default.GetLastKnownLocationAsync();
                if (CurrentLocation != null)
                {
                    MapSpan mapspan = new(CurrentLocation, 0.01, 0.01);
                    mainPage.mainmap.MoveToRegion(mapspan);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to move map: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public static Location GetLocationTest()
        {
            return Geolocation.Default.GetLastKnownLocationAsync().Result;
        }
    }
}
