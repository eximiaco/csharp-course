using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eximia.CsharpCourse.SeedWork;

public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void AddDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}

public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot where TKey : IComparable
{
    [NotMapped]
    private List<IDomainEvent> _domainEvents;

    protected AggregateRoot()
    {
        _domainEvents = new List<IDomainEvent>();
    }

    protected AggregateRoot(TKey id) : base(id)
    {
        _domainEvents = new List<IDomainEvent>();
    }

    [JsonIgnore]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}