namespace Domain.ValueObjects;

public partial record Identificacion
{
    private const int MaxLenght = 10;

    public string Value { get; init; }

    private Identificacion(string value) => Value = value;

    public static Identificacion? Create(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length > MaxLenght)
        {
            return null;
        }

        return new Identificacion(value);
    }

}
