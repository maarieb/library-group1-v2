using SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    abstract public class Person : Entity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string ?Mail { get; set; }
        public string ?PhoneNumber { get; set; }

        public Person()
        {
        }

        public Person(string lastName, string firstName)
        {
            LastName = lastName;
            FirstName = firstName;
        }

        public Person(string lastName, string firstName, string mail, string phoneNumber)
        {
            LastName = lastName;
            FirstName = firstName;
            Mail = mail;
            PhoneNumber = phoneNumber;
        }

        public Person(string lastName, string firstName, string mail, string phoneNumber, string login, string password)
        {
            LastName = lastName;
            FirstName = firstName;
            Mail = mail;
            PhoneNumber = phoneNumber;
        }

        public override string? ToString()
        {
            return $"{LastName}, {FirstName}, {Mail}, {PhoneNumber} ";
        }
    }
}
