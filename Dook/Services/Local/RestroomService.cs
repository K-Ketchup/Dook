using Dook.Shared.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dook.Services
{
    public static class RestroomService
    {
        static SQLiteAsyncConnection db;

        static async Task InitAsync()
        {
            if (db != null)
                return;

            //Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            try
            {
                await db.CreateTableAsync<Restroom>();
                await db.CreateTableAsync<Review>();
            }        
            catch(Exception ex) 
            { 
                Console.WriteLine(ex.ToString());
            }
        }

        public static async Task AddPinAsync(string name, string address, string username, double latitude, double longitude)
        {
            await InitAsync();
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

        public static async Task RemovePinAsync(int id)
        {
            await InitAsync();

            await db.DeleteAsync<Restroom>(id);
        }

        public static async Task<IEnumerable<Restroom>> GetPinAsync()
        {
            await InitAsync();

            var restroom = await db.Table<Restroom>().ToListAsync();
            return restroom;
        }

        public static async Task<Restroom> GetPinAsync(int id)
        {
            await InitAsync();

            var restroom = await db.Table<Restroom>()
                .FirstOrDefaultAsync(c =>  c.Id == id);

            return restroom;
        }
    }
}
