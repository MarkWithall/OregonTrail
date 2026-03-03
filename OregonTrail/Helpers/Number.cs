namespace OregonTrail;

internal readonly struct Number(double value) : IEquatable<Number>
{
    private readonly double _value = value;

    public static implicit operator double(Number n) => n._value;

    public static implicit operator Number(double n) => new(n);

    public override string ToString() => _value.ToString("0.##");

    public bool Equals(Number other) => _value.Equals(other._value);

    public override bool Equals(object? obj) =>
        obj is Number other && Equals(other);

    public override int GetHashCode() => _value.GetHashCode();

    public static bool operator ==(Number lhs, Number rhs) => lhs.Equals(rhs);

    public static bool operator !=(Number lhs, Number rhs) => !lhs.Equals(rhs);
}
