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
            //Bunu yapmazsak, sistem tablolar�m�zda veri varken g�ncelleme yapmak istemeyecek ve hata d�nd�recek. 
            //AutomaticMigrationDataLossAllowed de�erinin True olmas� tabloda veri olmas�na ra�men tablo �zerinde yap�sal de�i�iklik ger�ekle�tirmemize izin verecek.
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
