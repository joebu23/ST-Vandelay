using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace VandelayIndustries.DAL.Models
{
    [TableName("Buyer")]
    public class Buyer : IPerson
    {
        public int Id { get; set; }
    }
}