using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Model : DbContext
    {
        public DbSet<Staff> Employees { get; set; }

        public DbSet<Client> Clients { get; set; }

    }
}
