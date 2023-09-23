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
        public Command GetCurrentLocationCommand { get; }
        public Command MoveMapCommand { get; }
        public MainViewModel()
        {
            Title = "Map Controller";
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

            //Location location = new();
            //location = Geolocation.Default.GetLastKnownLocationAsync().Result;
            //if (location != null)
            //    return location;
                
            //return null;
        }
    }
}
