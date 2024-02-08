using API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Database;

public class ClienteConfig : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> cliente)
    {
        cliente.ToTable("clientes");

        cliente.HasKey(c => c.Id);
        cliente.Property(c => c.Id).ValueGeneratedOnAdd();

        cliente.HasData(new Cliente { Id = 1, Limite = 1000_00 });
        cliente.HasData(new Cliente { Id = 2, Limite = 800_00 });
        cliente.HasData(new Cliente { Id = 3, Limite = 10_000_00 });
        cliente.HasData(new Cliente { Id = 4, Limite = 100_000_00 });
        cliente.HasData(new Cliente { Id = 5, Limite = 5000_00 });
    }
}
