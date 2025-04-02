namespace PantheonAddonFramework.Configuration;

public class PicklistConfigurationValue : IConfigurationValue
{
    public string Name { get; }
    public string Description { get; }
    public int InitialIndex { get; }
    public IEnumerable<string> Values { get; }
    public Action<int> SelectionChanged { get; }

    public PicklistConfigurationValue(string name, string description, int initialIndex, IEnumerable<string> values, Action<int> selectionChanged)
    {
        Name = name;
        Description = description;
        InitialIndex = initialIndex;
        Values = values;
        SelectionChanged = selectionChanged;
    }
}