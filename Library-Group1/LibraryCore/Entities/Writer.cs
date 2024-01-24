using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Writer : Person
    {
        public string ?Rank { get; set; }
        public List<Book> ?Books { get; set; }
        public Writer()
        {
            Books = new List<Book>();
        }

        public Writer(string lastName, string firstName, string mail, string phoneNumber) : base(lastName, firstName, mail, phoneNumber)
        {
            Books = new List<Book>();
        }

        public Writer(string lastName, string firstName, string mail, string phoneNumber, string login, string password) : base(lastName, firstName, mail, phoneNumber, login, password)
        {
            Books = new List<Book>();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
