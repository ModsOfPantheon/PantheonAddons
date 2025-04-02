namespace PantheonAddonFramework.Configuration;

public class BoolConfigurationValue : IConfigurationValue
{
    public string Name { get; }
    public string Description { get; }
    public bool InitialValue { get; }
    public Action<bool> ValueChanged { get; }

    public BoolConfigurationValue(string name, string description, bool initialValue, Action<bool> valueChanged)
    {
        Name = name;
        Description = description;
        ValueChanged = valueChanged;
        InitialValue = initialValue;
    }
}