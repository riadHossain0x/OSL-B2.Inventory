namespace WholeSale.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        IsActive = c.Int(nullable: false),
                        ModifiedBy = c.Long(),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Image = c.String(maxLength: 256, unicode: false),
                        Details = c.String(maxLength: 256, unicode: false),
                        Quantity = c.Int(),
                        Critical_Qty = c.Int(),
                        BuyingPrice = c.Decimal(precision: 19, scale: 2),
                        SalePrice = c.Decimal(precision: 19, scale: 2),
                        CategoryId = c.Long(nullable: false),
                        IsActive = c.Int(nullable: false),
                        ModifiedBy = c.Long(),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.SaleDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        Quantity = c.Int(nullable: false),
                        SalePrice = c.Decimal(nullable: false, precision: 19, scale: 2),
                        BuyingPrice = c.Decimal(nullable: false, precision: 19, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 19, scale: 2),
                        SaleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CustomerId = c.Long(nullable: false),
                        SaleDate = c.DateTime(nullable: false),
                        GrandTotal = c.Decimal(nullable: false, precision: 19, scale: 2),
                        IsActive = c.Int(nullable: false),
                        ModifiedBy = c.Long(),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(),
                        Mobile = c.String(nullable: false, maxLength: 15, unicode: false),
                        Address = c.String(),
                        IsActive = c.Int(nullable: false),
                        ModifiedBy = c.Long(),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Mobile = c.String(nullable: false, maxLength: 15, unicode: false),
                        Address = c.String(maxLength: 256, unicode: false),
                        Details = c.String(maxLength: 256, unicode: false),
                        IsActive = c.Int(nullable: false),
                        ModifiedBy = c.Long(),
                        ModifiedDate = c.DateTime(),
                        CreatedBy = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleDetails", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.SaleDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.SaleDetails", new[] { "SaleId" });
            DropIndex("dbo.SaleDetails", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Customers");
            DropTable("dbo.Sales");
            DropTable("dbo.SaleDetails");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
