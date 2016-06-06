using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VandelayIndustries.DAL.Models;

namespace VandelayIndustries.DAL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, VandelayIndustries.Migrations.Configuration>());
        }

        public static DataContext Create()
        {
            return new DataContext();
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<SalesPerson> SalesPersons { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Seller> Sellers { get; set; }

    }
}