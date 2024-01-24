using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Admin : Person
    {
        public string ?Password { get; set; }
        public Admin()
        {
        }

        public Admin(string lastName, string firstName, string mail, string phoneNumber) : base(lastName, firstName, mail, phoneNumber)
        {
        }

        public Admin(string lastName, string firstName, string mail, string phoneNumber, string login, string password) : base(lastName, firstName, mail, phoneNumber, login, password)
        {
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
