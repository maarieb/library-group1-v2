using SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Domain : Entity
    {
        public string Name { get; set; }
        public string ?Description { get; set; }
        public List<Book> ?Books { get; set; }

        public Domain()
        {
            Books = new List<Book>();
        }

        public Domain(string name, string description)
        {
            Name = name;
            Description = description;
            Books = new List<Book>();
        }

        public override string? ToString()
        {
            return $"{Name} : {Description}";
        }
    }
}
