using Bücherei.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bücherei.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ausleihe>().HasKey(a => new
            {
                a.Id
            });
        }

        public DbSet<Ausleihe> Ausleihe{ get; set; }
        public DbSet<Buch> Buch{ get; set; }
        public DbSet<SchuelerIn> SchuelerIn{ get; set; }
    }
}
