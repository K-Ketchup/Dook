using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Dook.Model
{
    public class Review
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Username")]
        public string Username { get; set; }
        [Column("Stars")]
        public double Stars { get; set; }
        [Column("Text")]
        public string Text { get; set; }
        //[Column("Tags")]
        //public string Tags { get; set; }
    }
}
