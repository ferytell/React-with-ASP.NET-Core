﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class CustomerDTO
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public decimal TotalAmount { get; set; }
}