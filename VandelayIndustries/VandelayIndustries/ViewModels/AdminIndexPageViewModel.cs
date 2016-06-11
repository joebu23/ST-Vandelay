using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VandelayIndustries.DAL.Models;

namespace VandelayIndustries.ViewModels
{
    public class AdminIndexPageViewModel
    {
        public List<Transaction> Transactions { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}