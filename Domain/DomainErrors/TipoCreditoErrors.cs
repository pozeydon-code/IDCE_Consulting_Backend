using ErrorOr;

namespace Domain.DomainErrors;

public static partial class Errors
{
    public static class TipoCredito
    {
        public static Error CreateTipoCreditoError(string message = "Error") => Error.Validation("CrearTipoCredito.Failed", message);
        public static Error NombreBadFormat => Error.Validation("TipoCredito.Stock", "El nombre del Tipo de Credito no tiene un formato valido");
        public static Error TipoCreditoIdAlreadyExist => Error.Validation("TipoCredito.Codigo", "El codigo del Tipo de Credito que quiere ingresar ya esta registrado.");
        public static Error NotFound => Error.NotFound("TipoCredito.NotFound", "El Tipo de Credito con el codigo proporcionado no ha sido encontrado.");

    }
}
