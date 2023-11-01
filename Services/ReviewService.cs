using Dook.Shared.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dook.Services
{
    public static class ReviewService
    {
        static SQLiteAsyncConnection db;

        static async Task InitAsync()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Review>();
        }

        public static async Task AddReviewAsync(string username, double stars, string text)
        {
            await InitAsync();

            var review = new Review()
            {
                Username = username,
                Stars = stars,
                Text = text
            };

            var id = await db.InsertAsync(review);
        }

        public static async Task RemoveReviewAsync(int id)
        {
            await InitAsync();

            await db.DeleteAsync<Review>(id);
        }

        public static async Task<IEnumerable<Review>> GetReviewAsync()
        {
            await InitAsync();

            var review = await db.Table<Review>().ToListAsync();
            return review;
        }
    }
}
