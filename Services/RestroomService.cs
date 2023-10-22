using Dook.Model;
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
            if(db != null) 
                return;

            //Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Restroom>();
        }

        public static async Task AddPinAsync(string name, string address, string username, Location location)
        {
            await InitAsync();
            var wa = "wa";
            var restroom = new Restroom
            {
                Name = name,
                Address = address,
                Username = username,
                PinLocation = location
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
    }
}
