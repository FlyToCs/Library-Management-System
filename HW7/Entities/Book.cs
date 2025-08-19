using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Entities
{
    public class Book
    {
        private static int idSet;
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public bool IsBorrow { get; set; }

        public Book(string title, string author, string description)
        {
            Id = ++idSet;
            Title = title;
            Author = author;
            Description = description;
            IsBorrow = false;
        }


    }
}
