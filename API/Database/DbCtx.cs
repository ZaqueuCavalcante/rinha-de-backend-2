using API.Domain;
using Syki.Back.Settings;
using Microsoft.EntityFrameworkCore;

namespace API.Database;

public class DbCtx(DbContextOptions<DbCtx> options, DbSettings settings) : DbContext(options)
{
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(settings.ConnectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
