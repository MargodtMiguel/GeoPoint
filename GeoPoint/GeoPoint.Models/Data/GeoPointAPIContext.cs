using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GeoPoint.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeoPoint.Models.Data
{
    public class GeoPointAPIContext : IdentityDbContext<GeoPointUser>
    {
        public GeoPointAPIContext (DbContextOptions<GeoPointAPIContext> options)
            : base(options)
        {
        }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Friends> Friends { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
