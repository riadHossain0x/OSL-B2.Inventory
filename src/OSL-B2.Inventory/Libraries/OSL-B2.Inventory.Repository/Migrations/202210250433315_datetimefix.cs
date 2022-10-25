namespace OSL_B2.Inventory.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimefix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "ModifiedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Categories", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Products", "ModifiedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Products", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Purchases", "ModifiedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Purchases", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Sales", "ModifiedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Sales", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Customers", "ModifiedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Customers", "CreatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Sales", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Sales", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Purchases", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Purchases", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Products", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Categories", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Categories", "ModifiedDate", c => c.DateTime());
        }
    }
}
