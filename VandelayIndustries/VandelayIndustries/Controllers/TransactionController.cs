using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VandelayIndustries.DAL;
using VandelayIndustries.DAL.Models;
using VandelayIndustries.ViewModels;

namespace VandelayIndustries.Controllers
{
    public class TransactionController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Transaction
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Buyer).Include(t => t.SalesPerson).Include(t => t.Seller);
            return View(transactions.ToList());
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transaction/Create
        public ActionResult Create()
        {
            var model = new TransactionCreateViewModel(db);
            //ViewBag.Id = new SelectList(db.Buyers, "Id", "Name");
            //ViewBag.Id = new SelectList(db.SalesPersons, "Id", "Name");
            //ViewBag.Id = new SelectList(db.Sellers, "Id", "Name");
            return View(model);
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //public ActionResult Create([Bind(Include = "Date,BuyerId,SellerId,SalesPersonId")] Transaction transaction)
        public string Create(TransactionAddModel model)
        {
            if (ModelState.IsValid)
            {
                var newTransaction = new Transaction();
                newTransaction.Buyer = db.Buyers.Find(model.Buyer);
                newTransaction.Seller = db.Sellers.Find(model.Seller);
                newTransaction.SalesPerson = db.SalesPersons.Find(model.SalesPerson);
                newTransaction.Date = model.Date;
                foreach (var item in model.Items)
                {
                    newTransaction.Items.Add(db.Items.Find(item));
                }

                db.Transactions.Add(newTransaction);
                db.SaveChanges();
                return "good";
            }

            return "bad";
        }

        // GET: Transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Buyers, "Id", "Name", transaction.Id);
            ViewBag.Id = new SelectList(db.SalesPersons, "Id", "Name", transaction.Id);
            ViewBag.Id = new SelectList(db.Sellers, "Id", "Name", transaction.Id);
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,BuyerId,SellerId,SalesPersonId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Buyers, "Id", "Name", transaction.Id);
            ViewBag.Id = new SelectList(db.SalesPersons, "Id", "Name", transaction.Id);
            ViewBag.Id = new SelectList(db.Sellers, "Id", "Name", transaction.Id);
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
