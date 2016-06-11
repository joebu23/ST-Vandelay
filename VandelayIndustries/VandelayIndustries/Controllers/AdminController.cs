using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using VandelayIndustries.DAL;
using VandelayIndustries.DAL.Models;
using VandelayIndustries.ViewModels;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace VandelayIndustries.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.UpperMessage = "CSV Upload";

            var model = new AdminIndexPageViewModel(db);
            model.Transactions = db.Transactions.Include(t => t.Buyer).Include(t => t.SalesPerson).Include(t => t.Seller).Include(t => t.Items).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(AdminIndexPageViewModel data)
        {
            if (data.File != null)
            {
                ViewBag.Message = ReadFile(data.File);
            }

            var model = new AdminIndexPageViewModel(db);
            model.Transactions = db.Transactions.Include(t => t.Buyer).Include(t => t.SalesPerson).Include(t => t.Seller).Include(t => t.Items).ToList();

            return View(model);
        }

        [HttpPost]
        public string FilterSalesCommissionTable(AdminPost data)
        {
            DateTime? low = null;
            DateTime? high = null;

            if (data.SalesPersonCommissionDateRangeLow != null)
            {
                low = Convert.ToDateTime(data.SalesPersonCommissionDateRangeLow);
            }

            if (data.SalesPersonCommissionDateRangeHigh != null)
            {
                high = Convert.ToDateTime(data.SalesPersonCommissionDateRangeHigh);
            }

            List<Transaction> transactions;
            SalesPerson salesPerson;

            // filter transactions down by sales person first
            transactions = db.Transactions.Include(t => t.Items).Where(p => p.SalesPerson.Id == data.SalesPersonIdToGet).ToList();

            // filter transactions by date if supplied
            if (low != null)
            {
                transactions = transactions.Where(p => p.Date >= low).ToList();
            }

            if (high != null)
            {
                transactions = transactions.Where(p => p.Date <= high).ToList();
            }

            salesPerson = db.SalesPersons.Single(p => p.Id == data.SalesPersonIdToGet);

            var totalCommission = 0m;
            foreach (var transaction in transactions)
            {
                totalCommission += (transaction.TotalCharges * (salesPerson.Commission * 0.01m));
            }

            var returnData = @"<tr><td>" + salesPerson.Name + "</td><td>" + string.Format("{0:C}", totalCommission) + "</td></tr>";
            return returnData;
        }

        private string ReadFile(HttpPostedFileBase file)
        {
            try
            {
                StreamReader reader = new StreamReader(file.InputStream);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    switch (values[0].ToLower())
                    {
                        case "seller":
                            var newSeller = new Seller
                            {
                                Name = values[1],
                                Address = values[2],
                                Phone = values[3],
                                Email = values[4]
                            };
                            db.Sellers.Add(newSeller);
                            break;
                        case "buyer":
                            var newBuyer = new Buyer
                            {
                                Name = values[1],
                                Address = values[2],
                                Phone = values[3],
                                Email = values[4]
                            };
                            db.Buyers.Add(newBuyer);
                            break;
                        case "sales":
                            var newSalesPerson = new SalesPerson
                            {
                                Name = values[1],
                                Commission = Convert.ToDecimal(values[2])
                            };
                            db.SalesPersons.Add(newSalesPerson);
                            break;
                        case "item":
                            var newItem = new Item
                            {
                                Description = values[1],
                                Price = Convert.ToDecimal(values[2]),
                                Color = values[3],
                                Weight = float.Parse(values[4])
                            };
                            db.Items.Add(newItem);
                            break;
                        default:
                            break;
                    }
                }

                db.SaveChanges();
                return "Save Successful!";

            }
            catch (Exception er)
            {
                return "Sorry, Upload Failed.";
            }
        }

    }
}