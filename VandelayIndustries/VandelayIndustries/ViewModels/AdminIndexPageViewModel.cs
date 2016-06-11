using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VandelayIndustries.DAL;
using VandelayIndustries.DAL.Models;

namespace VandelayIndustries.ViewModels
{
    public class AdminIndexPageViewModel
    {
        private DataContext db;

        public IEnumerable<SelectListItem> Buyers
        {
            get
            {
                var buyers = db.Buyers.Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                });
                return buyers;
            }
        }

        public IEnumerable<SelectListItem> Sellers
        {
            get
            {
                var sellers = db.Sellers.Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                });
                return sellers;
            }
        }

        public IEnumerable<SelectListItem> SalesPersons
        {
            get
            {
                var sales = db.SalesPersons.Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                });
                return sales;
            }
        }

        public IEnumerable<SelectListItem> Items
        {
            get
            {
                var items = db.Items.Select(p => new SelectListItem()
                {
                    Text = p.Description,
                    Value = p.Id.ToString()
                });
                return items;
            }

        }

        public AdminIndexPageViewModel(DataContext _db)
        {
            db = _db;
        }

        public AdminIndexPageViewModel()
        { }

        public List<Transaction> Transactions { get; set; }

        public HttpPostedFileBase File { get; set; }

    }
}