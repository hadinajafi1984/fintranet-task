using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using congestion.calculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace congestion.calculator.Domain.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public AppDbContextFactory()
        {

        }

        private readonly IConfiguration Configuration;
        public AppDbContextFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public ApplicationContext CreateDbContext(string[] args)
        {
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = buildDir + @"\appsettings.json";
            //string filePath = @"D:\Freinds Movie\Backend Technical Test\netcore\appsettings.json";

            IConfiguration Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(filePath))
                .AddJsonFile("appSettings.json")
                .Build();


            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DBConnection"));

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            
        }
        
        public virtual DbSet<HoursAmount> HoursAmounts { get; set; }
        public virtual DbSet<TaxExemptVehicle> TaxExemptVehicles { get; set; }
        public virtual DbSet<TollFreeDate> FreeDates { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.NoAction;
        }
    }
}
