using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dook.Model
{
    public class Restroom
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Address")]
        public string Address { get; set; }
        [Column("Username")]
        public string Username { get; set; }
        [Column("Latitude")]
        public double Latitude { get; set; }
        [Column("Longitude")]
        public double Longitude { get; set; }
    }
}
