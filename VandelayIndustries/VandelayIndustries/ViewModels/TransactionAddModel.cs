using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VandelayIndustries.ViewModels
{
    public class TransactionAddModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Seller { get; set; }
        public int Buyer { get; set; }
        public int SalesPerson { get; set; }
        public List<int> Items { get; set; }
    }
}