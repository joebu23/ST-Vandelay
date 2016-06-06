﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace VandelayIndustries.DAL.Models
{
    [TableName("Item")]
    public class Item
    {
        public Item()
        {
            this.Transactions = new HashSet<Transaction>();    
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public float Weight { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}