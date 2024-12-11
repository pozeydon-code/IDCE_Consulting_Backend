using Application.Services.TipoCreditos;

namespace Application.Services.Operaciones;

public record OperacionResponse(
    int OperacionID,
    string? Identificacion,
    string? Nombre,
    string? TipoCredito,
    decimal? Monto,
    DateTime? FechaInicio,
    int? PlazoMeses,
    bool? Aprobado,
    DateTime? FechaRegistro,
    TipoCreditoResponse? TipoCreditoNavigation
);
