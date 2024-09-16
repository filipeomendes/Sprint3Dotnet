using Microsoft.EntityFrameworkCore;
using Sprint3Dotnet.Models;

namespace Sprint3Dotnet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Loja> Lojas { get; set; }
    }
}
