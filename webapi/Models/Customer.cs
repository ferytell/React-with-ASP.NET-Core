using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICrudRestaurant.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}