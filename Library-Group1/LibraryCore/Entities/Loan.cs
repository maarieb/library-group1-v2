using SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Loan : Entity
    {
        public DateTime StartDate { get; set; }
        public DateTime ?EndDate { get; set; }
        public Reader ?Reader { get; set; }
        public Book ?Book { get; set; }

        public Loan()
        {
        }

        public Loan(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public override string? ToString()
        {
            return $"{StartDate} : {EndDate}";
        }
    }
}
