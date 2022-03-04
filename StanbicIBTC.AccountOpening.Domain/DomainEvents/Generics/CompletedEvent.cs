namespace StanbicIBTC.AccountOpening.Domain.Events;

public class CompletedEvent<T> : DomainEvent
{
    public CompletedEvent(T item)
    {
        Item = item;
    }

    public T Item { get; }
}
