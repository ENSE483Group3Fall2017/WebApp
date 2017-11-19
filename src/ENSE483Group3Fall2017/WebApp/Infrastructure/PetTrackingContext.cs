using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL;
using JetBrains.Annotations;

namespace WebApp.Infrastructure
{
    public class PetTrackingContext : DbContext, IDbContext
    {
        public PetTrackingContext(DbContextOptions<PetTrackingContext> options) : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<TrackingInfo> TrackingInfos { get; set; }
    }
}
