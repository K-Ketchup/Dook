using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dook.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        public double CurrentLocationLatitude { get; set; } = 33.7705078125;
        public double CurrentLocationLongitude { get; set; } = -118.37253108273849;
        public Location CurrentLocation { get; set; }
        public Command GetCurrentLocationCommand { get; }
        public MainViewModel()
        {
            Title = "Map Controller";
            GetCurrentLocationCommand = new Command(async () => await GetLocationAsync());
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
    }
}
