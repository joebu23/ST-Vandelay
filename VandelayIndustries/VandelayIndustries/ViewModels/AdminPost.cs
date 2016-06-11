using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VandelayIndustries.ViewModels
{
    public class AdminPost
    {
        public string SalesPersonCommissionDateRangeLow { get; set; }

        public string SalesPersonCommissionDateRangeHigh { get; set; }

        public int SalesPersonIdToGet { get; set; }
    }
}