namespace VandelayIndustries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelUpdate2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Transactions", name: "BuyerId", newName: "Buyer_Id");
            RenameColumn(table: "dbo.Transactions", name: "SalesPersonId", newName: "SalesPerson_Id");
            RenameColumn(table: "dbo.Transactions", name: "SellerId", newName: "Seller_Id");
            RenameIndex(table: "dbo.Transactions", name: "IX_BuyerId", newName: "IX_Buyer_Id");
            RenameIndex(table: "dbo.Transactions", name: "IX_SalesPersonId", newName: "IX_SalesPerson_Id");
            RenameIndex(table: "dbo.Transactions", name: "IX_SellerId", newName: "IX_Seller_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Transactions", name: "IX_Seller_Id", newName: "IX_SellerId");
            RenameIndex(table: "dbo.Transactions", name: "IX_SalesPerson_Id", newName: "IX_SalesPersonId");
            RenameIndex(table: "dbo.Transactions", name: "IX_Buyer_Id", newName: "IX_BuyerId");
            RenameColumn(table: "dbo.Transactions", name: "Seller_Id", newName: "SellerId");
            RenameColumn(table: "dbo.Transactions", name: "SalesPerson_Id", newName: "SalesPersonId");
            RenameColumn(table: "dbo.Transactions", name: "Buyer_Id", newName: "BuyerId");
        }
    }
}
