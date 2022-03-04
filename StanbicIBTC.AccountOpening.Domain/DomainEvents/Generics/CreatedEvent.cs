namespace StanbicIBTC.AccountOpening.Domain.Events;

public class CreatedEvent<T> : DomainEvent
{
    public CreatedEvent(T item)
    {
        Item = item;
    }

    public T Item { get; }
}

