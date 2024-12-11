using Domain.Models;

namespace Application.Services.Operaciones;
public record CreateOperacionRequest(
    string Identificacion,
    string Nombre,
    string TipoCredito,
    decimal Monto,
    DateTime FechaInicio,
    int PlazoMeses,
    bool Aprobado,
    DateTime FechaRegistro) : IRequest<ErrorOr<Unit>>;
public record GetOperacionByIdRequest(int OperacionID) : IRequest<ErrorOr<OperacionResponse>>;
public record GetAllOperacionesRequest() : IRequest<ErrorOr<IReadOnlyList<OperacionResponse>>>;
public record UpdateOperacionRequest(
    int OperacionID,
    string Identificacion,
    string Nombre,
    string TipoCredito,
    decimal Monto,
    DateTime FechaInicio,
    int PlazoMeses,
    bool Aprobado,
    DateTime FechaRegistro) : IRequest<ErrorOr<Unit>>;
public record DeleteOperacionRequest(int OperacionID) : IRequest<ErrorOr<Unit>>;
