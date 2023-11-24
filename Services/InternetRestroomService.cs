using Dook.Shared.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace Dook.Services
{
    public static class InternetRestroomService
    {
        //static string Baseurl = DeviceInfo.Platform == DevicePlatform.Android ?
        //                                    "http://10.0.2.2:5000" : "https://localhost:5000";
        static string BaseUrl = "https://dookwebapp.azurewebsites.net/";
        static HttpClient client;

        static InternetRestroomService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        static Random random = new Random();

        public static async Task AddPinAsync(string name, string address, string username, double latitude, double longitude)
        {
            int id = random.Next(0, 100000);

            try
            {
                //Check to see if ID is a duplicate. If exception 404 returns, the id is safe.
                var randomRestroom = await client.GetStringAsync($"api/Restroom/{id}");
                await AddPinAsync(name, address, username, latitude, longitude);
            }
            catch(HttpRequestException ex)
            {
                var restroom = new Restroom
                {
                    Name = name,
                    Address = address,
                    Username = username,
                    Latitude = latitude,
                    Longitude = longitude,
                    Id = id
                };

                var json = JsonConvert.SerializeObject(restroom);
                var content =
                    new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Restroom", content);

                if (!response.IsSuccessStatusCode)
                    Debug.WriteLine("Response Success!");
            }
        }

        public static async Task RemovePinAsync(int id)
        {
            var response = await client.DeleteAsync($"api/Restroom/{id}");
            if (!response.IsSuccessStatusCode)
                Debug.WriteLine("Response Success!");
        }

        public static async Task<IEnumerable<Restroom>> GetPinAsync()
        {
            var json = await client.GetStringAsync("api/Restroom");
            var restrooms = JsonConvert.DeserializeObject<IEnumerable<Restroom>>(json);
            return restrooms;
        }

        public static async Task<Restroom> GetSingularPinAsync(int id)
        {
            var json = await client.GetStringAsync($"api/Restroom/{id}");
            var restroom = JsonConvert.DeserializeObject<Restroom>(json);
            return restroom;
        }
    }
}
