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
        transacao.Property(t => t.Id).UseSerialColumn();

        transacao.Property(t => t.Tipo).HasColumnType("CHAR(1)");
        transacao.Property(t => t.Descricao).HasColumnType("VARCHAR(10)");

        transacao.Property(t => t.RealizadaEm)
            .HasColumnType("TIMESTAMP")
            .HasDefaultValueSql("NOW()");

        transacao.HasIndex(t => t.ClienteId);
    }
}
