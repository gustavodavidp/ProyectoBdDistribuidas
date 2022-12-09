using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetrasBlog.Infraestructure.Entities
{
    public class Books
    {
        public class response
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public Data Detail { get; set; }
            public List<BooksDetail> Books { get; set; }
        }
        public class Data
        {
            public int ID_LOG { get; set; }
            public int Error { get; set; }
            public string Total { get; set; }
            public string Page { get; set; }
            public string Message { get; set; }
            public List<BooksDetail> Books { get; set; }
        }
        public class BooksDetail
        {
            public int ID_LIBRO { get; set; }
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public string Isbn13 { get; set; }
            public string Price { get; set; }
            public string Image { get; set; }
            public string Url { get; set; }
        }
    }
}
