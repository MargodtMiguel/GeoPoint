using GeoPoint.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeoPoint.API
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GeoPointAPIContext>
    {
        GeoPointAPIContext IDesignTimeDbContextFactory<GeoPointAPIContext>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .Build();

            var builder = new DbContextOptionsBuilder<GeoPointAPIContext>();

            var connectionString = configuration.GetConnectionString("GeoPointAPIContext");

            builder.UseSqlServer(connectionString);

            return new GeoPointAPIContext(builder.Options);
        }

    }
}
