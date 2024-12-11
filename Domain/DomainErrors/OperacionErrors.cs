using ErrorOr;

namespace Domain.DomainErrors;

public static partial class Errors
{
    public static class Operacion
    {
        public static Error CreateOperacionError(string message = "Error") => Error.Validation("CrearOperacion.Failed", message);
        public static Error NombreBadFormat => Error.Validation("Operacion.Stock", "El nombre no tiene un formato valido");
        public static Error IdentificacionBadFormat => Error.Validation("Identificacion.Stock", "La Identificacion no tiene un formato valido");
        public static Error OperacionIdAlreadyExist => Error.Validation("Operacion.Codigo", "El codigo de Operacion que quiere ingresar ya esta registrado.");
        public static Error NotFound => Error.NotFound("Operacion.NotFound", "El operaciono con el id proporcionado no ha sido encontrado.");

    }
}
