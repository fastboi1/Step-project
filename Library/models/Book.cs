using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.models
{
    internal class Book : BookID
    {
     

        public string Title { get; set; }

        public Author Author { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Book(int id, string title, Author author, DateTime releaseDate)
        {
            ID = id;
            Title = title;
            Author = author;
            ReleaseDate = releaseDate;
        }

        

        public override string ToString()
        {
            return $"ID: {ID},Title: {Title}, Author: {Author}, Release Date: {ReleaseDate.ToShortDateString()}";
        }


    }
}
