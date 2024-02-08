using API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Database;

public class TransacaoConfig : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> transacao)
    {
        transacao.ToTable("transacoes");

        transacao.HasKey(t => t.Id);
        transacao.Property(t => t.Id).ValueGeneratedOnAdd();
    }
}
