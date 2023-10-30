using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICrudRestaurant.Models
{
    public class Food
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}