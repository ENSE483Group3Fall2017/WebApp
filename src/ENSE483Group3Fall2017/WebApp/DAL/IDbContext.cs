using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace WebApp.DAL
{
    public interface IDbContext : IDisposable
    {
        DbSet<Pet> Pets { get; }

        DbSet<TrackingInfo> TrackingInfos { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task SaveChangesAsync();

    }
}
