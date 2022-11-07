namespace OSL_B2.Inventory.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchase : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PurchaseDetails", "ModifiedBy");
            DropColumn("dbo.PurchaseDetails", "ModifiedDate");
            DropColumn("dbo.PurchaseDetails", "CreatedBy");
            DropColumn("dbo.PurchaseDetails", "CreatedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseDetails", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseDetails", "CreatedBy", c => c.Long(nullable: false));
            AddColumn("dbo.PurchaseDetails", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseDetails", "ModifiedBy", c => c.Long(nullable: false));
        }
    }
}
