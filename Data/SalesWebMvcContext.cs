using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SalesWebMVC.Models;

namespace SalesWebMVC.Data
{
    public class SalesWebMvcContext(DbContextOptions<SalesWebMvcContext> options) : DbContext(options)
    {
         public DbSet<Department> Department { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=saleswebmvcdb;user=MVCuser;password=mVC#1246;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });
        }
    }
}
