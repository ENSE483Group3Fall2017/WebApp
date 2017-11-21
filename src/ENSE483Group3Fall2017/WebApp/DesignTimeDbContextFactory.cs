using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Infrastructure;

namespace WebApp
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PetTrackingContext>
    {
        public PetTrackingContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var builder = new DbContextOptionsBuilder<PetTrackingContext>();
            var connectionString = configuration.GetConnectionString("PetTrackingDatabase");
            builder.UseSqlServer(connectionString);
            return new PetTrackingContext(builder.Options);
        }
    }
}
