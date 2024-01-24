using LibraryCore.Enums;
using SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string ?Description { get; set; }
        public int PagesNumber { get; set; }
        public BookState? State { get; set; }
        public Writer ?Writer { get; set; }
        public Domain ?Domain { get; set; }
        public List<Loan> ?Loans { get; set; }
        public Book()
        {
            Loans = new List<Loan>();
        }

        public Book(string title, int pagesNumber, Writer? writer, Domain? domain)
        {
            Title = title;
            PagesNumber = pagesNumber;
            Writer = writer;
            Domain = domain;
            Loans = new List<Loan>();
        }

        public Book(string title, string description, Writer writer, Domain domain)
        {
            Title = title;
            Description = description;
            Writer = writer;
            Domain = domain;
            Loans = new List<Loan>();
        }

        public override string? ToString()
        {
            return $"{Title} : {Description}\nAuteur : {Writer.LastName} {Writer.FirstName}\n";
        }
    }
}
