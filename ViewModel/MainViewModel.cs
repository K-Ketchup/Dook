using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dook.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        public string currentLocation { get; }
        public Command GetCurrentLocationCommand { get; }
        public MainViewModel()
        {
            Title = "Map Controller";
            GetCurrentLocationCommand = new Command(async () => await GetTextAsync());
        }

        public async Task<string> GetCurrentLocationAsync()
        {
            if (IsBusy)
                return null;

            try
            {
                IsBusy = true;
                Location location = await Geolocation.Default.GetLastKnownLocationAsync();
                if (location != null)
                    return $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
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

            return null;
        }

        public async Task<string> GetTextAsync()
        {
            if (IsBusy)
                return null;

            try
            {
                IsBusy = true;
                return "bruh";
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

            return null;
        }
    }
}
