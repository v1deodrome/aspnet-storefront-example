using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using INET2005_FinalProject.Models;

namespace INET2005_FinalProject.Data
{
    public class INET2005_FinalProjectContext : DbContext
    {
        public INET2005_FinalProjectContext (DbContextOptions<INET2005_FinalProjectContext> options)
            : base(options)
        {
        }

        public DbSet<INET2005_FinalProject.Models.Car> Car { get; set; } = default!;

        public DbSet<INET2005_FinalProject.Models.CarType> CarType { get; set; } = default!;
    }
}
