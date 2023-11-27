using DookToilets.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DookToilets.Services
{
    public static class RestroomService
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;

            //Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Restroom>();
        }

        public static async Task AddPin(string name, string address, string username, double latitude, double longitude)
        {
            await Init();
            var restroom = new Restroom
            {
                Name = name,
                Address = address,
                Username = username,
                Latitude = latitude,
                Longitude = longitude
            };

            var id = await db.InsertAsync(restroom);
        }

        public static async Task RemovePin(int id)
        {
            await Init();

            await db.DeleteAsync<Restroom>(id);
        }

        public static async Task<IEnumerable<Restroom>> GetPin()
        {
            await Init();

            var restroom = await db.Table<Restroom>().ToListAsync();
            return restroom;
        }
    }
}
