using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICrudRestaurant.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public int FoodId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Quantity { get; set; }


        public Customer Customer { get; set; }
        public Food Food { get; set; }

    }
}