using Microsoft.EntityFrameworkCore;
using net_ef_videogame.Classes;
using net_ef_videogame.Helpers;

namespace net_ef_videogame.Database
{
    public class MovieContext : DbContext
    {
        private string server = "FEDERICO-OVERSI\\SQLEXPRESS";
        private string db = "VideogameEF";
        private string userId = "sa";
        private string password = "overside2023";

        public DbSet<Videogame> Videogames { get; set; }
        public DbSet<SoftwareHouse> Software_houses { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server={server};Database={db};User ID={userId};Password={password};Integrated Security=False; TrustServerCertificate=True");
        }

    }
}
