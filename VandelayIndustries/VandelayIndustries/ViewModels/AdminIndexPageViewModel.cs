﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public List<SalesPersonSales> SalesPersonSalesList
        {
            get
            {
                var returnList = new List<SalesPersonSales>();
                var salesPersons = db.SalesPersons;

                foreach (var person in salesPersons)
                {
                    var newPerson = new SalesPersonSales();
                    newPerson.SalesPerson = person;

                    newPerson.CommissionEarned = 0;

                    var transactions = db.Transactions.Where(p => p.SalesPerson.Id == person.Id).ToList();
                    foreach(var transaction in transactions)
                    {
                        newPerson.CommissionEarned += (transaction.TotalCharges * (person.Commission * 0.01m));
                    }

                    returnList.Add(newPerson);
                }

                return returnList;
            }
        }

        public class SalesPersonSales
        {
            public SalesPerson SalesPerson { get; set; }

            public decimal CommissionEarned { get; set; }
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