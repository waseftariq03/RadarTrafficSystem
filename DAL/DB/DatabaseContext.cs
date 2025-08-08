using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DB

{

    public class DatabaseContext : DbContext

    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options)

            : base(options)

        {

        }

        public DbSet<Violation> Violations { get; set; }

    }

}

