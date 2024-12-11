using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Models;

public sealed class Operacion : AggregateRoot
{
    #region Propiedades
    public int OperacionID { get; private set; }
    public Identificacion? Identificacion { get; private set; }
    public NombreOperacion? Nombre { get; private set; }
    public string? TipoCredito { get; private set; }
    public decimal? Monto { get; private set; }
    public DateTime? FechaInicio { get; private set; }
    public int? PlazoMeses { get; private set; }
    public bool? Aprobado { get; private set; }
    public DateTime? FechaRegistro { get; private set; }

    // public TipoCredito? TipoCreditoNavigation { get; private set; }
    #endregion

    #region Constructores
    private Operacion()
    {

    }

    public Operacion(int operacionID, Identificacion? identificacion, NombreOperacion? nombre, string? tipoCredito, decimal? monto, DateTime? fechaInicio, int? plazoMeses, bool? aprobado, DateTime? fechaRegistro)
    {
        OperacionID = operacionID;
        Identificacion = identificacion;
        Nombre = nombre;
        TipoCredito = tipoCredito;
        Monto = monto;
        FechaInicio = fechaInicio;
        PlazoMeses = plazoMeses;
        Aprobado = aprobado;
        FechaRegistro = fechaRegistro;
    }

    public static Operacion UpdateOperacion(int operacionID, Identificacion identificacion, NombreOperacion nombre, string tipoCredito, decimal monto, DateTime fechaInicio, int plazoMeses, bool aprobado, DateTime fechaRegistro)
    {
        return new Operacion(operacionID, identificacion, nombre, tipoCredito, monto, fechaInicio, plazoMeses, aprobado, fechaRegistro);
    }

    #endregion
}
