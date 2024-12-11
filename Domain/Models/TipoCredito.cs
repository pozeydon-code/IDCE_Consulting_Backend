using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Models;

public sealed class TipoCredito : AggregateRoot
{
    #region Propiedades
    public string Codigo { get; private set; }
    public NombreTipoCredito? Nombre { get; private set; }

    // public ICollection<Operacion>? Operaciones { get; private set; }
    #endregion

    #region Constructores
    private TipoCredito()
    {

    }

    public TipoCredito(string codigo, NombreTipoCredito? nombre)
    {
        Codigo = codigo;
        Nombre = nombre;
    }

    public static TipoCredito UpdateTipoCredito(string codigo, NombreTipoCredito nombre)
    {
        return new TipoCredito(codigo, nombre);
    }

    #endregion
}
