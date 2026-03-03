namespace OregonTrail;

internal sealed class Text(int size)
{
    private string _value = string.Empty;

    public void SetValue(string value) =>
        _value = new string(value.Take(size).ToArray());

    public static implicit operator string(Text text) => text._value;

    public static implicit operator Text(string str)
    {
        Text t = new(str.Length);
        t.SetValue(str);
        return t;
    }

    public override string ToString() => _value;
}
