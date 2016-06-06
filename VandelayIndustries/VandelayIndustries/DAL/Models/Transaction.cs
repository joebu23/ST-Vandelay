using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace VandelayIndustries.DAL.Models
{
    [TableName("Transaction")]
    public class Transaction
    {
        public Transaction()
        {
            this.Items = new HashSet<Item>();
        }

        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        [ForeignKey("Id")]
        public Buyer Buyer { get; set; }

        public int BuyerId { get; set; }

        [ForeignKey("Id")]
        public Seller Seller { get; set; }
        public int SellerId { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        [ForeignKey("Id")]
        public SalesPerson SalesPerson { get; set; }
        public int SalesPersonId { get; set; }
    }
}