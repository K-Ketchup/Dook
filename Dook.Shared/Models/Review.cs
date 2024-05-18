using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Dook.Shared.Models
{
    public class Review : INotifyPropertyChanged
    {
        private int id;
        private string username;
        private double stars;
        private string text;
        private int restroomid;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        [Column("Id")]
        public int Id
        {
            get { return id; } 
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        [Column("Username")]
        public string Username
        {
            get { return username; }
            set
            {
                if (value != username)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }
        [Column("Stars")]
        public double Stars
        {
            get { return stars; }
            set
            {
                if (value != stars)
                {
                    stars = value;
                    OnPropertyChanged("Stars");
                }
            }
        }
        [Column("Text")]
        public string Text
        {
            get { return text; }
            set
            {
                if (value != text)
                {
                    text = value;
                    OnPropertyChanged("Text");
                }
            }
        }
        [Column("RestroomId")]
        public int RestroomId
        {
            get { return restroomid; }
            set
            {
                if (value != restroomid)
                {
                    restroomid = value;
                    OnPropertyChanged("RestroomId");
                }
            }
        }
        //[Column("Tags")]
        //public string Tags { get; set; }
    }
}
