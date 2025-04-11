using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Book
    {

        #region Props
        public string? ISBN { get; set; }
        public string? Title { get; set; }
        public string[]? Authors { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        #endregion

        #region Constructor
        public Book(string? iSBN, string? title, string[]? authors, DateTime publicationDate, decimal price)
        {
            ISBN = iSBN;
            Title = title;
            Authors = authors;
            PublicationDate = publicationDate;
            Price = price;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return $" The Book Data is : Title : {Title} \n ISBN : {ISBN}\n Price: {Price} \n  Publication_Date{PublicationDate} \n Authors are {Authors}";
        } 
        #endregion

    }
}
