using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class Book
    {
        
        public int Id { get; set; }
        public string Bookname { get; set; }
        public string Author { get; set; }
        public string Catagory { get; set; }
        public DateTime publishDate { get; set; }
    }
}
