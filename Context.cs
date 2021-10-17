using System;
using Microsoft.EntityFrameworkCore;

namespace MyDataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() => Database.EnsureCreated();
        public DbSet<Client> Clients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=SomeDataBase;Trusted_Connection=true");
        }
    }
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}