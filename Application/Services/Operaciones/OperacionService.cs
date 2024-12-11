using Domain.Models;
using Domain.Primitives;
using Domain.Repositories;
using Domain.DomainErrors;
using Domain.ValueObjects;
using Application.Services.TipoCreditos;


namespace Application.Services.Operaciones;

public class OperacionService :
    IRequestHandler<GetAllOperacionesRequest, ErrorOr<IReadOnlyList<OperacionResponse>>>,
    IRequestHandler<GetOperacionByIdRequest, ErrorOr<OperacionResponse>>,
    IRequestHandler<CreateOperacionRequest, ErrorOr<Unit>>,
    IRequestHandler<UpdateOperacionRequest, ErrorOr<Unit>>,
    IRequestHandler<DeleteOperacionRequest, ErrorOr<Unit>>
{
    private readonly IOperacionRepository _operacionRepository;
    private readonly ITipoCreditoRepository _tipoCreditoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OperacionService(IOperacionRepository operacionRepository, IUnitOfWork unitOfWork, ITipoCreditoRepository tipoCreditoRepository)
    {
        _operacionRepository = operacionRepository ?? throw new ArgumentNullException(nameof(operacionRepository));
        _tipoCreditoRepository = tipoCreditoRepository ?? throw new ArgumentNullException(nameof(tipoCreditoRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    //Create Product
    public async Task<ErrorOr<Unit>> Handle(CreateOperacionRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (Identificacion.Create(request.Identificacion) is not Identificacion identificacion)
                return Errors.Operacion.NombreBadFormat;

            if (NombreOperacion.Create(request.Nombre) is not NombreOperacion operacionName)
                return Errors.Operacion.NombreBadFormat;

            Operacion Operacion = new(
                0,
                identificacion,
                operacionName,
                request.TipoCredito,
                request.Monto,
                request.FechaInicio,
                request.PlazoMeses,
                request.Aprobado,
                request.FechaRegistro
            );

            _operacionRepository.AddAsync(Operacion);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            return Errors.Operacion.CreateOperacionError(ex.Message);
        }

    }

    //GetAll Products
    public async Task<ErrorOr<IReadOnlyList<OperacionResponse>>> Handle(GetAllOperacionesRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<Operacion> operaciones = await _operacionRepository.GetAllAsync();

        // Cargar todos los TipoCredito necesarios de una sola vez
        var tipoCreditos = await _tipoCreditoRepository.GetAllAsync();
        var tipoCreditoDict = tipoCreditos.ToDictionary(tc => tc.Codigo);

        var responses = operaciones.Select(operacion => new OperacionResponse(
            operacion.OperacionID,
            operacion.Identificacion!.Value,
            operacion.Nombre!.Value,
            operacion.TipoCredito,
            operacion.Monto,
            operacion.FechaInicio,
            operacion.PlazoMeses,
            operacion.Aprobado,
            operacion.FechaRegistro,
            new TipoCreditoResponse(
                tipoCreditoDict.TryGetValue(operacion.TipoCredito!, out var tipoCredito) ? tipoCredito.Codigo : "DefaultCodigo",
                tipoCreditoDict.TryGetValue(operacion.TipoCredito!, out tipoCredito) ? tipoCredito.Nombre!.Value : "DefaultNombre"
            )
        )).ToList();

        return responses;
    }

    //GetById Product
    public async Task<ErrorOr<OperacionResponse>> Handle(GetOperacionByIdRequest request, CancellationToken cancellationToken)
    {
        if (await _operacionRepository.GetByIdAsync(request.OperacionID) is not Operacion Operacion)
            return Errors.Operacion.NotFound;

        // OperacionResponse operacionResponse = new OperacionResponse(
        //     0,
        //     Operacion.Identificacion.Value,
        //     Operacion.Nombre.Value,
        //     Operacion.TipoCredito,
        //     Operacion.Monto,
        //     Operacion.FechaInicio,
        //     Operacion.PlazoMeses,
        //     Operacion.Aprobado,
        //     Operacion.FechaRegistro,
        //     new TipoCreditoResponse(
        //         Operacion.TipoCreditoNavigation.Codigo,
        //         Operacion.TipoCreditoNavigation.Nombre.Value
        //     )
        // );
        OperacionResponse operacionResponse = new OperacionResponse(
            0,
            Operacion.Identificacion!.Value,
            Operacion.Nombre!.Value,
            Operacion.TipoCredito,
            Operacion.Monto,
            Operacion.FechaInicio,
            Operacion.PlazoMeses,
            Operacion.Aprobado,
            Operacion.FechaRegistro,
            new TipoCreditoResponse(
                 (await _tipoCreditoRepository.GetByIdAsync(Operacion.TipoCredito!))?.Codigo ?? "DefaultCodigo",
                (await _tipoCreditoRepository.GetByIdAsync(Operacion.TipoCredito!))?.Nombre!.Value ?? "DefaultNombre"
            )
        );
        return operacionResponse;
    }

    //Delete Product
    public async Task<ErrorOr<Unit>> Handle(DeleteOperacionRequest request, CancellationToken cancellationToken)
    {
        if (await _operacionRepository.GetByIdAsync(request.OperacionID) is not Operacion Operacion)
            return Errors.Operacion.NotFound;

        _operacionRepository.Delete(Operacion);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    //Update Product
    public async Task<ErrorOr<Unit>> Handle(UpdateOperacionRequest request, CancellationToken cancellationToken)
    {
        if (!await _operacionRepository.ExistAsync(request.OperacionID)!)
            return Errors.Operacion.NotFound;

        if (!await _tipoCreditoRepository.ExistAsync(request.TipoCredito)!)
            return Errors.TipoCredito.NotFound;

        if (Identificacion.Create(request.Identificacion) is not Identificacion identificacion)
            return Errors.Operacion.NombreBadFormat;

        if (NombreOperacion.Create(request.Nombre) is not NombreOperacion operacionName)
            return Errors.Operacion.NombreBadFormat;


        Operacion operacion = Operacion.UpdateOperacion(
            request.OperacionID,
            identificacion,
            operacionName,
            request.TipoCredito,
            request.Monto,
            request.FechaInicio,
            request.PlazoMeses,
            request.Aprobado,
            request.FechaRegistro
        );

        _operacionRepository.Update(operacion);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
