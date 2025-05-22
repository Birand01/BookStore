using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;

namespace BackEnd.Data
{
    public static class ApplicationContext
    {
        public static List<Book> Books{get;set;}
        static ApplicationContext()
        {
            Books=new List<Book>()
            {
                new Book(){Id=1,Title="A",Price=100},
                new Book(){Id=2,Title="B",Price=200},
                new Book(){Id=3,Title="C",Price=300},
            };
        }
    }
}