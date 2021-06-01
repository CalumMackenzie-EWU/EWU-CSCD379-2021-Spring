using Microsoft.EntityFrameworkCore;

namespace SecretSanta.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext//cal: note that it is convention to name it SecretSantaDbContext
    {

        public DbSet<Group> Groups => Set<Group>();

        public DbContext(): base(new DbContextOptionsBuilder<DbContext>().UseSqlite("Data Source=main.db").Options)
        { 

        }

    }
}