namespace VandelayIndustries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Transaction_Id", "dbo.Transactions");
            DropIndex("dbo.Items", new[] { "Transaction_Id" });
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
            
            DropColumn("dbo.Items", "Transaction_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Transaction_Id", c => c.Int());
            DropForeignKey("dbo.TransactionItems", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.TransactionItems", "Transaction_Id", "dbo.Transactions");
            DropIndex("dbo.TransactionItems", new[] { "Item_Id" });
            DropIndex("dbo.TransactionItems", new[] { "Transaction_Id" });
            DropTable("dbo.TransactionItems");
            CreateIndex("dbo.Items", "Transaction_Id");
            AddForeignKey("dbo.Items", "Transaction_Id", "dbo.Transactions", "Id");
        }
    }
}
