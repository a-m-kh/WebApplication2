using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Wallet> wallets { get; set; }
        public DbSet<Coin> coins { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=WalletDB;Trusted_Connection=True;");
        }
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
    }
}
