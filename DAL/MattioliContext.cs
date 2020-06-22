using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
   public class MattioliContext : DbContext
    {
        public MattioliContext(DbContextOptions<MattioliContext> options) : base(options)
        {
        }

        public DbSet<Staff> Staff { get; set; }
        public DbSet<Client> Clients { get; set; }

    }
}
