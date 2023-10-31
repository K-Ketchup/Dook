using Dook.Shared.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MonkeyCache.FileStore;

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

        public static Task<IEnumerable<Restroom>> GetPinAsync() =>
            GetAsync<IEnumerable<Restroom>>("api/Restroom", "getrestroom");

        public static async Task<string> GetPinAsync(int id)
        {
            var response = await client.GetStringAsync($"api/Restroom/{id}");
            return response;
        }

        static async Task<T> GetAsync<T>(string url, string key, int mins = 1, bool forceRefresh = false)
        {
            var json = string.Empty;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(key);
            else if (!forceRefresh && !Barrel.Current.IsExpired(key))
                json = Barrel.Current.Get<string>(key);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync(url);

                    Barrel.Current.Add(key, json, TimeSpan.FromMinutes(mins));
                }
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw ex;
            }
        }
    }
}
