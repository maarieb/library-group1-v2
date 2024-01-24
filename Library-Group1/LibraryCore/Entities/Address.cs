using SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Address : Entity
    {
        public string ?Appartment { get; set; }
        public string ?Street { get; set; }
        public string ?City { get; set; }
        public string ?ZipCode { get; set; }
        public string ?Country { get; set; }
        public List<Reader> ?Readers { get; set; }

        public Address()
        {
            Readers = new List<Reader>();
        }

        public Address(string appartment, string street, string city, string zipCode, string country)
        {
            Appartment = appartment;
            Street = street;
            City = city;
            ZipCode = zipCode;
            Country = country;
            Readers = new List<Reader>();
        }

        public override string? ToString()
        {
            return $"{Appartment}, {Street}, {City}, {ZipCode}, {Country}";
        }
    }
}
