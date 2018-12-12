using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_MyCurrency.Models;

namespace API_MyCurrency.Models
{
    public class DBContext : DbContext
    {
        public DBContext (DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<API_MyCurrency.Models.Alert> Alert { get; set; }

        public DbSet<API_MyCurrency.Models.Position> Position { get; set; }
    }
}
