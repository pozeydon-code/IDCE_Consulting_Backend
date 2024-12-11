
using Application.Services.TipoCreditos;

namespace Web.API.Controllers;

[Route("TipoCreditos")]
public class TipoCreditos : ApiController
{
    private readonly ISender _mediator;

    public TipoCreditos(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var UserResult = await _mediator.Send(new GetAllTipoCreditosRequest());

        return UserResult.Match(
            tipoCreditos => Ok(tipoCreditos),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string codigo)
    {
        var UserResult = await _mediator.Send(new GetTipoCreditoByIdRequest(codigo));

        return UserResult.Match(
            tipocredito => Ok(tipocredito),
            errors => Problem(errors)
        );
    }

    [HttpPost("Crear")]
    public async Task<IActionResult> Create([FromBody] CreateTipoCreditoRequest command)
    {
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            userId => Ok(userId),
            errors => Problem(errors)
        );
    }

    [HttpPut("Modificar/{codigo}")]
    public async Task<IActionResult> Update(string codigo, [FromBody] UpdateTipoCreditoRequest command)
    {
        if (command.Codigo != codigo)
        {
            List<Error> errors = new()
            {
                Error.Validation("TipoCredito.UpdateInvalido", "El Id no es igual al id.")
            };
            return Problem(errors);
        }

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            tipocreditoId => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("Delete/{codigo}")]
    public async Task<IActionResult> Delete(string codigo)
    {
        var deleteResult = await _mediator.Send(new DeleteTipoCreditoRequest(codigo));

        return deleteResult.Match(
            opreacionId => NoContent(),
            errors => Problem(errors)
        );
    }

}
