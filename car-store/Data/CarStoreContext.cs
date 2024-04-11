using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarStore.Models;

namespace CarStore.Data
{
    public class CarStoreContext : DbContext
    {
        public CarStoreContext (DbContextOptions<CarStoreContext> options)
            : base(options)
        {
        }

        public DbSet<CarStore.Models.Car> Car { get; set; } = default!;

        public DbSet<CarStore.Models.CarType> CarType { get; set; } = default!;
    }
}
