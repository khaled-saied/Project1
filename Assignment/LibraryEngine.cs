using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{

    //public delegate string ProcessBooksDelegate<T>(T B);
    public delegate T ProcessBooksDelegate<T>(Book B);


    internal class LibraryEngine
    {
        public static void ProcessBooks<T>(List<Book> books, ProcessBooksDelegate<T> fptr)
        {
            if (books == null || fptr is null) return;
            foreach (Book B in books)
                Console.WriteLine(fptr(B));
        }


        public static void ProcessBooks<T>(List<Book> books, Func<Book, T> fptr)
        {
            if (books == null || fptr is null) return;
            foreach (Book B in books)
                Console.WriteLine(fptr(B));
        }
    }
}
