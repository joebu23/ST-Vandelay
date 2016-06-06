using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace VandelayIndustries.DAL.Models
{
    [TableName("SalesPersons")]
    public class SalesPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Commission { get; set; }
    }
}