using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Reader : Person
    {
        public string ?Password { get; set; }
        public Address ?Address { get; set; }
        public List<Loan> ?Loans { get; set; }
        public Reader()
        {
            Loans = new List<Loan>();
        }

        public Reader(string lastName, string firstName, string mail, string phoneNumber) : base(lastName, firstName, mail, phoneNumber)
        {
            Loans = new List<Loan>();
        }

        public Reader(string lastName, string firstName, string mail, string phoneNumber, string login, string password) : base(lastName, firstName, mail, phoneNumber, login, password)
        {
            Loans = new List<Loan>();
        }

        public override string? ToString()
        {
            return base.ToString() + Address;
        }
    }
}
