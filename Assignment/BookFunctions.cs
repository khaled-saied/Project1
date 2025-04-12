using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class BookFunctions
    {
        public static string GetTitle(Book b) => b?.Title ?? "No Title";
        public static string GetAuthors(Book b) => b?.Authors?.Length > 0 ? string.Join(":", b.Authors) : "No Authors";
        public static decimal GetPrice(Book b) => b?.Price > 0 ? b.Price : 0;
        public static string GetISBN(Book b)
        {
            if (b is not null && b.ISBN is not null)
                return b.ISBN;

            return "Invalid ISBN!!!";
        }
    }
}
