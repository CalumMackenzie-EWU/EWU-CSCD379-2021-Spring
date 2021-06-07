using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext//cal: note that it is convention to name it SecretSantaDbContext
    {

        

        //public DbContext(): base(new DbContextOptionsBuilder<DbContext>().UseSqlite(@"Data Source=..\..\src\SecretSanta.Data\main.db").Options){}
        //public DbContext(): base(new DbContextOptionsBuilder<DbContext>().UseSqlite(@"Data Source=..\..\..\..\..\src\SecretSanta.Data\main.db").Options){}
        public DbContext(): base(new DbContextOptionsBuilder<DbContext>().UseSqlite(@"Data Source=main.db").Options){}
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Gift> Gifts => Set<Gift>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Assignment> Assignments => Set<Assignment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            /*
            modelBuilder.Entity<Gift>()
            .HasAlternateKey(item=>new {item.Title,item.Url})
            .HasName("AlternateKey_Title&Url");
            */
            
            modelBuilder.Entity<Gift>().HasIndex(item => new {item.Title, item.Url}).IsUnique();
            modelBuilder.Entity<Group>().HasIndex(item => new {item.Name}).IsUnique();
            //modelBuilder.Entity<User>().HasIndex(item => new {item.Email}).IsUnique();
            modelBuilder.Entity<Assignment>().HasIndex(item => new {item.GiftDue}).IsUnique();
            
        }

    }
}