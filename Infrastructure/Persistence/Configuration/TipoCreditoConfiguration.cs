using Domain.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configuration;

public class TipoCreditoConfiguration : IEntityTypeConfiguration<TipoCredito>
{
    public void Configure(EntityTypeBuilder<TipoCredito> builder)
    {
        builder.ToTable("TipoCredito");

        builder.HasKey(tC => tC.Codigo);

        builder.Property(tC => tC.Codigo).HasMaxLength(10);

        builder.Property(tC => tC.Nombre).HasConversion(
            nombre => nombre.Value,
            value => NombreTipoCredito.Create(value)!).HasMaxLength(60);

    }
}
