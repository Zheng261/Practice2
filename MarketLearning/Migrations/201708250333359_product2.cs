namespace MarketLearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductId", c => c.Int());
        }
    }
}
