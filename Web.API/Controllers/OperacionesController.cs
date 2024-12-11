
using Application.Services.Operaciones;

namespace Web.API.Controllers;

[Route("operaciones")]
public class Operaciones : ApiController
{
    private readonly ISender _mediator;

    public Operaciones(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var UserResult = await _mediator.Send(new GetAllOperacionesRequest());

        return UserResult.Match(
            operaciones => Ok(operaciones),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var UserResult = await _mediator.Send(new GetOperacionByIdRequest(id));

        return UserResult.Match(
            operacion => Ok(operacion),
            errors => Problem(errors)
        );
    }

    [HttpPost("Crear")]
    public async Task<IActionResult> Create([FromBody] CreateOperacionRequest command)
    {
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            userId => Ok(userId),
            errors => Problem(errors)
        );
    }

    [HttpPut("Modificar/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOperacionRequest command)
    {
        if (command.OperacionID != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Operacion.UpdateInvalido", "El Id no es igual al id.")
            };
            return Problem(errors);
        }

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            operacionId => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteOperacionRequest(id));

        return deleteResult.Match(
            opreacionId => NoContent(),
            errors => Problem(errors)
        );
    }

}
