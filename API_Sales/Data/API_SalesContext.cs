using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_Sales.Modèle;

namespace API_Sales.Data
{
    public class API_SalesContext : DbContext
    {
        public API_SalesContext (DbContextOptions<API_SalesContext> options)
            : base(options)
        {
        }

        public DbSet<API_Sales.Modèle.Consolec> Consolec { get; set; } = default!;

        public DbSet<API_Sales.Modèle.Manufacturer>? Manufacturer { get; set; }

        public DbSet<API_Sales.Modèle.Sale>? Sale { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder options) =>

        //options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=API_Sales.Data;Trusted_Connection=True;MultipleActiveResultSets=true");
    }
}
