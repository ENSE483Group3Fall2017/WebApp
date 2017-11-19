using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DAL
{
    public interface IDbContext
    {

        DbSet<Pet> Pets { get; }

        DbSet<TrackingInfo> TrackingInfos { get; }
    }
}
