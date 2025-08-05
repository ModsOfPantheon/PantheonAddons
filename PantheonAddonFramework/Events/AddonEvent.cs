namespace PantheonAddonFramework.Events;

public class AddonEvent
{
    private event Action Event = delegate { };
    
    public void Subscribe(Action handler) => Event += handler;
    public void Unsubscribe(Action handler) => Event -= handler;
    internal void Raise() => Event();
}

public class AddonEvent<T>
{
    private event Action<T> Event = delegate { }; // Prevents null reference exceptions

    public void Subscribe(Action<T> handler) => Event += handler;
    public void Unsubscribe(Action<T> handler) => Event -= handler;
    internal void Raise(T arg) => Event(arg);
}