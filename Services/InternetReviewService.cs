using Dook.Shared.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dook.Services
{
    public static class InternetReviewService
    {
        //static string Baseurl = DeviceInfo.Platform == DevicePlatform.Android ?
        //                                    "http://10.0.2.2:5000" : "http://localhost:5000";
        static string BaseUrl = "https://dookwebapp.azurewebsites.net/";
        static HttpClient client;

        static InternetReviewService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        static Random random = new Random();

        public static async Task AddReviewAsync(string username, double stars, string text, int restroomid)
        {
            //Check to see if ID is a duplicate
            int idNum = random.Next(0, 100000);
            var randomReview = await client.GetStringAsync($"api/Restroom/{idNum}");

            while (randomReview != "")
            {
                idNum = random.Next(0, 100000);
                randomReview = await client.GetStringAsync($"api/Restroom/{idNum}");
            }

            var review = new Review()
            {
                Username = username,
                Stars = stars,
                Text = text,
                Id = idNum,
                RestroomId = restroomid 
            };

            var json = JsonConvert.SerializeObject(review);
            var content =
                new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Restroom", content);

            if (!response.IsSuccessStatusCode)
            {

            }
        }

        public static async Task RemoveReviewAsync(int id)
        {
            var response = await client.DeleteAsync($"api/Restroom/{id}");
            if (!response.IsSuccessStatusCode)
            {

            }
        }

        public static async Task<IEnumerable<Review>> GetReviewAsync(int restId)
        {
            //var json = await client.GetStringAsync($"api/Restroom/{restId}");
            var json = await client.GetStringAsync($"api/Restroom/{restId}");
            var reviews = JsonConvert.DeserializeObject<IEnumerable<Review>>(json);
            return reviews;
        }
    }
}
