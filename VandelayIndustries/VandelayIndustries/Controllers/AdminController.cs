using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using VandelayIndustries.DAL;
using VandelayIndustries.DAL.Models;
using VandelayIndustries.ViewModels;

namespace VandelayIndustries.Controllers
{
    public class AdminController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Message = "CSV Upload";
            return View();
        }

        [HttpPost]
        public ActionResult Index(FileUploadViewModel datafile)
        {
            if(datafile.File != null)
            {
                ViewBag.Message = ReadFile(datafile.File);
                
            }

            return View();
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

                    switch (values[0])
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
                            break;
                        default:
                            break;
                    }
                }

                db.SaveChanges();
                return "Save Successful!";

            } catch (Exception er)
            {
                return "Sorry, Upload Failed.";
            }
        }

    }
}