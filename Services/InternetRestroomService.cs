using Dook.Shared.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dook.Services
{
    public static class InternetRestroomService
    {
        //static string Baseurl = DeviceInfo.Platform == DevicePlatform.Android ?
        //                                    "http://10.0.2.2:7151" : "https://localhost:7151";
        static string BaseUrl = "https://thankful-field-0380b931e.4.azurestaticapps.net";
        static HttpClient client;

        static InternetRestroomService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public static async Task AddPinAsync(string name, string address, string username, double latitude, double longitude)
        {
            var restroom = new Restroom
            {
                Name = name,
                Address = address,
                Username = username,
                Latitude = latitude,
                Longitude = longitude
            };
        }

        public static async Task RemovePinAsync(int id)
        {

        }

        public static async Task<IEnumerable<Restroom>> GetPinAsync()
        {
            var json = await client.GetStringAsync("api/Restroom");
            var restrooms = JsonConvert.DeserializeObject<IEnumerable<Restroom>>(json);
            return restrooms;
        }
    }
}
