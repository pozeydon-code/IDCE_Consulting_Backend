using Domain.Models;
using Domain.Primitives;
using Domain.Repositories;
using Domain.DomainErrors;
using Domain.ValueObjects;


namespace Application.Services.TipoCreditos;

public class TipoCreditoService :
    IRequestHandler<GetAllTipoCreditosRequest, ErrorOr<IReadOnlyList<TipoCreditoResponse>>>,
    IRequestHandler<GetTipoCreditoByIdRequest, ErrorOr<TipoCreditoResponse>>,
    IRequestHandler<CreateTipoCreditoRequest, ErrorOr<TipoCreditoResponse>>,
    IRequestHandler<UpdateTipoCreditoRequest, ErrorOr<Unit>>,
    IRequestHandler<DeleteTipoCreditoRequest, ErrorOr<Unit>>
{
    private readonly ITipoCreditoRepository _tipoCreditoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TipoCreditoService(ITipoCreditoRepository tipoCreditoRepository, IUnitOfWork unitOfWork)
    {
        _tipoCreditoRepository = tipoCreditoRepository ?? throw new ArgumentNullException(nameof(tipoCreditoRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    //Create Product
    public async Task<ErrorOr<TipoCreditoResponse>> Handle(CreateTipoCreditoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (await _tipoCreditoRepository.ExistAsync(request.Codigo)!)
                return Errors.TipoCredito.TipoCreditoIdAlreadyExist;

            if (NombreTipoCredito.Create(request.Nombre) is not NombreTipoCredito tipoCreditoName)
                return Errors.TipoCredito.NombreBadFormat;

            TipoCredito TipoCredito = new(
                request.Codigo,
                tipoCreditoName
            );

            _tipoCreditoRepository.AddAsync(TipoCredito);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new TipoCreditoResponse(
                request.Codigo,
                tipoCreditoName.Value
            );
        }
        catch (Exception ex)
        {
            return Errors.TipoCredito.CreateTipoCreditoError(ex.Message);
        }

    }

    //GetAll Products
    public async Task<ErrorOr<IReadOnlyList<TipoCreditoResponse>>> Handle(GetAllTipoCreditosRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<TipoCredito> tipocredito = await _tipoCreditoRepository.GetAllAsync();

        return tipocredito.Select(tipocredito => new TipoCreditoResponse(
                   tipocredito.Codigo,
                   tipocredito.Nombre!.Value
               )).ToList();
    }

    //GetById Product
    public async Task<ErrorOr<TipoCreditoResponse>> Handle(GetTipoCreditoByIdRequest request, CancellationToken cancellationToken)
    {
        if (await _tipoCreditoRepository.GetByIdAsync(request.Codigo) is not TipoCredito tipoCredito)
            return Errors.TipoCredito.NotFound;

        TipoCreditoResponse tipoCreditoResponse = new TipoCreditoResponse(
            tipoCredito.Codigo,
            tipoCredito.Nombre!.Value
        );

        return tipoCreditoResponse;
    }

    //Delete Product
    public async Task<ErrorOr<Unit>> Handle(DeleteTipoCreditoRequest request, CancellationToken cancellationToken)
    {
        if (await _tipoCreditoRepository.GetByIdAsync(request.Codigo) is not TipoCredito TipoCredito)
            return Errors.TipoCredito.NotFound;

        _tipoCreditoRepository.Delete(TipoCredito);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    //Update Product
    public async Task<ErrorOr<Unit>> Handle(UpdateTipoCreditoRequest request, CancellationToken cancellationToken)
    {
        if (!await _tipoCreditoRepository.ExistAsync(request.Codigo)!)
            return Errors.TipoCredito.NotFound;

        if (NombreTipoCredito.Create(request.Nombre) is not NombreTipoCredito tipoCreditoName)
            return Errors.TipoCredito.NombreBadFormat;


        TipoCredito tipocredito = TipoCredito.UpdateTipoCredito(
            request.Codigo,
            tipoCreditoName
        );

        _tipoCreditoRepository.Update(tipocredito);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
