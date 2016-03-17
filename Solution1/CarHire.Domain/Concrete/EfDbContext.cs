using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarHire.Domain.Entities;
using System.Data.Entity;

namespace CarHire.Domain.Concrete
{
    public class EfDbContext:DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rental> Rent { get; set; }
    }
}
