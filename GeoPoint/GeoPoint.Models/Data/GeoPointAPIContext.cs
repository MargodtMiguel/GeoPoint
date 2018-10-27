using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoPoint.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace GeoPoint.Models
{
    public class GeoPointAPIContext : DbContext
    {
        public GeoPointAPIContext (DbContextOptions<GeoPointAPIContext> options)
            : base(options)
        {
        }
        public DbSet<tblUsers> Users { get; set; }
        public DbSet<tblScores> Scores { get; set; }
        public DbSet<tblFriends> Friends { get; set; }

        //FLUENT API 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Seed();
        }

    }
}
