using Microsoft.EntityFrameworkCore;
using SignalR_Backend.Models;
using System.Collections.Generic;

namespace SignalR_Backend.Data
{
    public class BackendContext:DbContext
    {
        public DbSet<tb_name_surname> tb_name_surname { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Port=5442;Username=postgres;Password=saadet2000;Database=postgres");
        }
    }
}
