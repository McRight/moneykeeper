using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PrpProject.Models
{
    public class ProjectContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hystory> Hystories { get; set; }
        public DbSet<MoneyManagerItem> MoneyManagerItems { get; set; }
    }
}