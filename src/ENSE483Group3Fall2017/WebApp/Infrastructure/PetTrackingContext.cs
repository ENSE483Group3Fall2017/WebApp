using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using WebApp.DAL;

namespace WebApp.Infrastructure
{
    public class PetTrackingContext : DbContext, IDbContext
    {
        public PetTrackingContext(DbContextOptions<PetTrackingContext> options) 
            : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<TrackingInfo> TrackingInfos { get; set; }

        public Task<IDbContextTransaction> BeginTransactionAsync() => Database.BeginTransactionAsync();

        public void CommitTransactionAsync() => Database.CommitTransaction();

        public void RollbackTransaction() => Database.RollbackTransaction();

        public Task SaveChangesAsync() => base.SaveChangesAsync();
    }
}
