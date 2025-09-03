using ClientManager.src.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientManager.src.Infrastructure;

public class AplicationDbContext(DbContextOptions<AplicationDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasIndex(c => c.Cpf).IsUnique();
        });
        base.OnModelCreating(modelBuilder);
    }

}
