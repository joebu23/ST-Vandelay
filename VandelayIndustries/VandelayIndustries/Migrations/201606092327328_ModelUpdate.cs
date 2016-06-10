namespace VandelayIndustries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransactionItems", "Transaction_Id", "dbo.Transactions");
            DropForeignKey("dbo.TransactionItems", "Item_Id", "dbo.Items");
            DropIndex("dbo.TransactionItems", new[] { "Transaction_Id" });
            DropIndex("dbo.TransactionItems", new[] { "Item_Id" });
            AddColumn("dbo.Items", "Transaction_Id", c => c.Int());
            CreateIndex("dbo.Items", "Transaction_Id");
            AddForeignKey("dbo.Items", "Transaction_Id", "dbo.Transactions", "Id");
            DropTable("dbo.TransactionItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TransactionItems",
                c => new
                    {
                        Transaction_Id = c.Int(nullable: false),
                        Item_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Transaction_Id, t.Item_Id });
            
            DropForeignKey("dbo.Items", "Transaction_Id", "dbo.Transactions");
            DropIndex("dbo.Items", new[] { "Transaction_Id" });
            DropColumn("dbo.Items", "Transaction_Id");
            CreateIndex("dbo.TransactionItems", "Item_Id");
            CreateIndex("dbo.TransactionItems", "Transaction_Id");
            AddForeignKey("dbo.TransactionItems", "Item_Id", "dbo.Items", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TransactionItems", "Transaction_Id", "dbo.Transactions", "Id", cascadeDelete: true);
        }
    }
}
