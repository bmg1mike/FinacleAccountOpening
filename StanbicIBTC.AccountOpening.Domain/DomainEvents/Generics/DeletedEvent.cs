namespace StanbicIBTC.AccountOpening.Domain.Events;

public class DeletedEvent<T> : DomainEvent
{
    public DeletedEvent(T item)
    {
        Item = item;
    }

    public T Item { get; }
}

