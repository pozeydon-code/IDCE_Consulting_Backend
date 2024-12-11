namespace Domain.ValueObjects;

public partial record NombreOperacion
{
    private const int DefaultLenght = 100;

    public string Value { get; init; }

    private NombreOperacion(string value) => Value = value;

    public static NombreOperacion? Create(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length > DefaultLenght)
        {
            return null;
        }

        return new NombreOperacion(value);
    }

}
