using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class TransactionDTO
{
    public int TransactionId { get; set; }
    public int CustomerId { get; set; }
    public int FoodId { get; set; }
    public string TransactionDate { get; set; }
    public int Quantity { get; set; }
    public string CustomerName { get; set; }
    public string FoodName { get; set; }

    public decimal Amount { get; set; }
}