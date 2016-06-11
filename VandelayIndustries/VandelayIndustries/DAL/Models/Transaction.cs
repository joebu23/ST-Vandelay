using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public virtual Buyer Buyer { get; set; }

        [Required]
        public virtual Seller Seller { get; set; }

        [Required]
        public ICollection<Item> Items { get; set; }

        [Required]
        public virtual SalesPerson SalesPerson { get; set; }
    }
}