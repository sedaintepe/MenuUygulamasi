namespace MenuTakip.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MenuTakip.Models.VerimDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //Bunu yapmazsak, sistem tablolarýmýzda veri varken güncelleme yapmak istemeyecek ve hata döndürecek. 
            //AutomaticMigrationDataLossAllowed deðerinin True olmasý tabloda veri olmasýna raðmen tablo üzerinde yapýsal deðiþiklik gerçekleþtirmemize izin verecek.
            AutomaticMigrationDataLossAllowed = true;
          
            ContextKey = "MenuTakip.Models.VerimDbContext";
        }

        protected override void Seed(MenuTakip.Models.VerimDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
