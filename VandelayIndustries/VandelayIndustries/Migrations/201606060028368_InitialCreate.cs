namespace VandelayIndustries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buyers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Color = c.String(),
                        Weight = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        BuyerId = c.Int(nullable: false),
                        SellerId = c.Int(nullable: false),
                        SalesPersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buyers", t => t.Id)
                .ForeignKey("dbo.SalesPersons", t => t.Id)
                .ForeignKey("dbo.Sellers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SalesPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Commission = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionItems",
                c => new
                    {
                        Transaction_Id = c.Int(nullable: false),
                        Item_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Transaction_Id, t.Item_Id })
                .ForeignKey("dbo.Transactions", t => t.Transaction_Id, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.Item_Id, cascadeDelete: true)
                .Index(t => t.Transaction_Id)
                .Index(t => t.Item_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Id", "dbo.Sellers");
            DropForeignKey("dbo.Transactions", "Id", "dbo.SalesPersons");
            DropForeignKey("dbo.TransactionItems", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.TransactionItems", "Transaction_Id", "dbo.Transactions");
            DropForeignKey("dbo.Transactions", "Id", "dbo.Buyers");
            DropIndex("dbo.TransactionItems", new[] { "Item_Id" });
            DropIndex("dbo.TransactionItems", new[] { "Transaction_Id" });
            DropIndex("dbo.Transactions", new[] { "Id" });
            DropTable("dbo.TransactionItems");
            DropTable("dbo.Sellers");
            DropTable("dbo.SalesPersons");
            DropTable("dbo.Transactions");
            DropTable("dbo.Items");
            DropTable("dbo.Buyers");
        }
    }
}
