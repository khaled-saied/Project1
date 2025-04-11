namespace Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<Book> books = new List<Book>
            {
                new Book("12345", "C# Basics",  [ "Ali", "Mona" ], DateTime.Now, 1220)
            };

            ProcessBooksDelegate<decimal> processBooks = BookFunctions.GetPrice;

            LibraryEngine.ProcessBooks(books, processBooks);

        }
    }
}
