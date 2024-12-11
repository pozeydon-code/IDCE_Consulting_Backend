namespace Domain.ValueObjects;

public partial record NombreTipoCredito
{
    private const int DefaultLenght = 60;

    public string Value { get; init; }

    private NombreTipoCredito(string value) => Value = value;

    public static NombreTipoCredito? Create(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length > DefaultLenght)
        {
            return null;
        }

        return new NombreTipoCredito(value);
    }

}
