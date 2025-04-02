namespace PantheonAddonFramework.Configuration;

public class FloatConfigurationValue : IConfigurationValue
{
    public string Name { get; }
    public string Description { get; }
    public float InitialValue { get; }
    public float MinValue { get; }
    public float MaxValue { get; }
    public float StepAmount { get; }
    public Action<float> ValueChanged { get; }    
    
    public FloatConfigurationValue(string name, string description, float initialValue, float minValue, float maxValue, float stepAmount, Action<float> valueChanged)
    {
        Name = name;
        Description = description;
        InitialValue = initialValue;
        MinValue = minValue;
        MaxValue = maxValue;
        StepAmount = stepAmount;
        ValueChanged = valueChanged;
    }
}