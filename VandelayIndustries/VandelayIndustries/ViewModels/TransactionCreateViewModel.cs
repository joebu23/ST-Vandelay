using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VandelayIndustries.DAL;
using VandelayIndustries.DAL.Models;

namespace VandelayIndustries.ViewModels
{
    public class TransactionCreateViewModel
    {
        private DataContext db;
        public DateTime? Date { get; set; }
        public IEnumerable<SelectListItem> Buyers { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }
        public IEnumerable<SelectListItem> Sellers { get; set; }
        public IEnumerable<SelectListItem> SalesPersons { get; set; }


        public TransactionCreateViewModel(DataContext _db)
        {
            db = _db;
            CreateModel();
        }

        public TransactionCreateViewModel()
        {

        }

        private void CreateModel()
        {

            Buyers = db.Buyers.Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });

            Items = db.Items.Select(p => new SelectListItem()
            {
                Text = p.Description,
                Value = p.Id.ToString()
            });

            Sellers = db.Sellers.Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });

            SalesPersons = db.SalesPersons.Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });
        }
    }
}