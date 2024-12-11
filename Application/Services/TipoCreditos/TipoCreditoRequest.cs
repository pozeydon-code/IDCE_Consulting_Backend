using Domain.Models;

namespace Application.Services.TipoCreditos;
public record CreateTipoCreditoRequest(
    string Codigo,
    string Nombre) : IRequest<ErrorOr<TipoCreditoResponse>>;
public record GetTipoCreditoByIdRequest(string Codigo) : IRequest<ErrorOr<TipoCreditoResponse>>;
public record GetAllTipoCreditosRequest() : IRequest<ErrorOr<IReadOnlyList<TipoCreditoResponse>>>;
public record UpdateTipoCreditoRequest(
    string Codigo,
    string Nombre) : IRequest<ErrorOr<Unit>>;
public record DeleteTipoCreditoRequest(string Codigo) : IRequest<ErrorOr<Unit>>;
