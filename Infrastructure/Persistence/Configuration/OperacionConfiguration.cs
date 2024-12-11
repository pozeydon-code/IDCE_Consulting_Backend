using Domain.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configuration;

public class OperacionConfiguration : IEntityTypeConfiguration<Operacion>
{
    public void Configure(EntityTypeBuilder<Operacion> builder)
    {
        builder.ToTable("Operaciones");

        builder.HasKey(o => o.OperacionID);

        builder.Property(o => o.OperacionID).ValueGeneratedOnAdd();

        builder.Property(o => o.Identificacion).HasConversion(
            identificacion => identificacion.Value,
            value => Identificacion.Create(value)!)
            .HasMaxLength(10).IsRequired(false);

        builder.Property(o => o.Nombre).HasConversion(
            nombre => nombre.Value,
            value => NombreOperacion.Create(value)!)
            .HasMaxLength(100).IsRequired(false);


        builder.Property(u => u.TipoCredito).HasMaxLength(15).IsRequired(false);

        // builder.HasOne(o => o.TipoCreditoNavigation)
        //     .WithMany(tC => tC.Operaciones)
        //     .HasForeignKey(o => o.TipoCredito)
        //     .OnDelete(DeleteBehavior.Restrict);

        builder.Property(o => o.Monto).IsRequired(false);

        builder.Property(o => o.FechaInicio).IsRequired(false);

        builder.Property(o => o.PlazoMeses).IsRequired(false);

        builder.Property(o => o.Aprobado).IsRequired(false);

        builder.Property(o => o.FechaRegistro).IsRequired(false);

    }
}
