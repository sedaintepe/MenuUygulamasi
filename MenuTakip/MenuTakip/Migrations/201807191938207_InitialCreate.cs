namespace MenuTakip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuModels",
                c => new
                    {
                        menuId = c.Int(nullable: false, identity: true),
                        menuFiyat = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.menuId);
            
            CreateTable(
                "dbo.YemekMenuModels",
                c => new
                    {
                        yemekMenuId = c.Int(nullable: false, identity: true),
                        tarih = c.DateTime(nullable: false),
                        Menu_menuId = c.Int(),
                        Yemek_yemekId = c.Int(),
                    })
                .PrimaryKey(t => t.yemekMenuId)
                .ForeignKey("dbo.MenuModels", t => t.Menu_menuId)
                .ForeignKey("dbo.YemekModels", t => t.Yemek_yemekId)
                .Index(t => t.Menu_menuId)
                .Index(t => t.Yemek_yemekId);
            
            CreateTable(
                "dbo.YemekModels",
                c => new
                    {
                        yemekId = c.Int(nullable: false, identity: true),
                        yemekAd = c.String(),
                    })
                .PrimaryKey(t => t.yemekId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.YemekMenuModels", "Yemek_yemekId", "dbo.YemekModels");
            DropForeignKey("dbo.YemekMenuModels", "Menu_menuId", "dbo.MenuModels");
            DropIndex("dbo.YemekMenuModels", new[] { "Yemek_yemekId" });
            DropIndex("dbo.YemekMenuModels", new[] { "Menu_menuId" });
            DropTable("dbo.YemekModels");
            DropTable("dbo.YemekMenuModels");
            DropTable("dbo.MenuModels");
        }
    }
}
