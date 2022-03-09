using AppCore.DataAccess.Configs;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.EntityFramework.Contexts {
    public class ETradeContext : DbContext {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyDetail> CurrencyDetails { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            ConnectionConfig.ConnectionString = "Server=DESKTOP-09H2BRQ;Database=MoneyExChangeDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(ConnectionConfig.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<CurrencyDetail>().HasOne(cd => cd.currencies)
                .WithMany(a => a.CurrencyDetail).HasForeignKey(cd => cd.CurrencyId).OnDelete(DeleteBehavior.NoAction);
        
        }
    }
}
