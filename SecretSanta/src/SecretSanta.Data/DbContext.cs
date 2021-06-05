using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext//cal: note that it is convention to name it SecretSantaDbContext
    {

        

        public DbContext(): base(new DbContextOptionsBuilder<DbContext>().UseSqlite(@"Data Source=..\..\src\SecretSanta.Data\main.db").Options)
        { 

        }
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Gift> Gifts => Set<Gift>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Assignment> Assignments => Set<Assignment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  

        }

    }
}