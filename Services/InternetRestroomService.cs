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
        //                                    "http://10.0.2.2:5000" : "http://localhost:5000";
        static string BaseUrl = "https://dookwebapp.azurewebsites.net/";
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

            var json = JsonConvert.SerializeObject(restroom);
            var content =
                new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Restroom", content);

            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Yipee");    
            }
        }

        public static async Task RemovePinAsync(int id)
        {
            var response = await client.DeleteAsync($"api/Restroom/{id}");
            if (!response.IsSuccessStatusCode)
            {

            }
        }

        public static async Task<IEnumerable<Restroom>> GetPinAsync()
        {
            try
            {
                var json = await client.GetStringAsync("api/Restroom");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            var json2 = await client.GetStringAsync("api/Restroom");
            var restrooms = JsonConvert.DeserializeObject<IEnumerable<Restroom>>(json2);
            return restrooms;
        }
    }
}
