using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Repository.Entities;

namespace WorkerShop.Repository.DbContexts
{
    public class WorkerContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Client>  Clients { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    
        public WorkerContext(DbContextOptions options)
            :base(options)
        {

        }
    }
}
