using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dook.Model
{
    public class Review
    {
        public string Username { get; set; }
        public int Stars { get; set; }
        public string Text { get; set; }
        public string TagList { get; set; }
    }
}
