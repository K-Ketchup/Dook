using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dook.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        public string currentLocation;
        public Command GetCurrentLocationCommand { get; }
        public MainViewModel()
        {
            Title = "Map Controller";
            GetCurrentLocationCommand = new Command(async () => await GetCurrentLocationAsync());
        }

        async Task GetCurrentLocationAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Location location = await Geolocation.Default.GetLastKnownLocationAsync();
                if (location != null)
                    currentLocation = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
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
