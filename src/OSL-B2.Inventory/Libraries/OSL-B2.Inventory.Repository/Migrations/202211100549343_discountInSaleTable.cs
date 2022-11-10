namespace OSL_B2.Inventory.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class discountInSaleTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "DiscountTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "DiscountTotal");
        }
    }
}
