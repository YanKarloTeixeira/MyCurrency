using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APIMyCurrency.Models;

namespace APIMyCurrency.Models
{
    public class DBContext : DbContext
    {
        public DBContext (DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Alert> Alert { get; set; }

        public DbSet<Position> Position { get; set; }
    }
}
