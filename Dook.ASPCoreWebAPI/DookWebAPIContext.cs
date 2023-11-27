using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;
using Dook.Shared.Models;

namespace Dook.ASPCoreWebAPI
{
    public class DookWebAPIContext : DbContext
    {
        public DookWebAPIContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Restroom> Restrooms { get; set; } = null!;

        public DbSet<Dook.Shared.Models.Review> Review { get; set; } = default!;

        //this is a method that was used during "add scaffolding" for the restroomcontroller. Removed but might need again"
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //           .SetBasePath(Directory.GetCurrentDirectory())
        //           .AddJsonFile("appsettings.json")
        //           .Build();
        //        var connectionString = configuration.GetConnectionString("DbCoreConnectionString");
        //        optionsBuilder.UseSqlServer(connectionString);
        //    }
        //}
    }
}
