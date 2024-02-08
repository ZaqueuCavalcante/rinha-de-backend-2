using API.Domain;
using Syki.Back.Settings;
using Microsoft.EntityFrameworkCore;

namespace API.Database;

public class DbCtx : DbContext
{
    private readonly DbSettings _settings;
    public DbCtx(DbContextOptions<DbCtx> options, DbSettings settings) : base(options)
    {
        _settings = settings;
    }

    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_settings.ConnectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("rinha");

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
