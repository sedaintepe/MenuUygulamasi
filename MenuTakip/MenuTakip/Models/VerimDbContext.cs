using MenuTakip.Kullanici;
using MenuTakip.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MenuTakip.Models
{
    public class VerimDbContext : IdentityDbContext<UserApps>

    {
        public VerimDbContext()
            : base("VerimDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VerimDbContext, Configuration>("VerimDbContext"));
        }

        
            public System.Data.Entity.DbSet<YemekModel> Yemekler { get; set; }
            public System.Data.Entity.DbSet<MenuModel> Menuler { get; set; }
            public System.Data.Entity.DbSet<YemekMenuModel> YemekMenuler { get; set; }
            public System.Data.Entity.DbSet<KisiMenuModel> KisiMenuler { get; set; }
          //  public System.Data.Entity.DbSet<MaasModel> Maaslar{ get; set; }
           // public System.Data.Entity.DbSet<KisiMaasModel> KisiMaaslar { get; set; }
            
            //protected override void OnModelCreating(DbModelBuilder modelBuilder)
            //{
            //    modelBuilder.Entity<MenuModel>.HasRequired(c => c.yemekMenuId).WithMany().WillCascadeOnDelete(false);
            //    modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
                
            //       //    modelBuilder.Entity<Card>()
            ////.HasRequired(c => c.Stage)
            ////.WithMany()
            ////.WillCascadeOnDelete(false);

            ////    modelBuilder.Entity<Side>()
            ////        .HasRequired(s => s.Stage)
            ////        .WithMany()
            ////        .WillCascadeOnDelete(false);
            //    base.OnModelCreating(modelBuilder);
            //}
           
    }
}