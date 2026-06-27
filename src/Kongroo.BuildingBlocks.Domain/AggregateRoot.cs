using System.ComponentModel.DataAnnotations.Schema;

namespace Kongroo.BuildingBlocks.Domain;

/// <summary>Base type for aggregate roots: the consistency boundary and source of domain events.</summary>
/// <typeparam name="TId">The strongly-typed identifier.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IHasDomainEvents
    where TId : IStronglyTypedId, IEquatable<TId>
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <inheritdoc />
    [NotMapped]
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <inheritdoc />
    public void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>Buffers a domain event for dispatch after persistence.</summary>
    /// <param name="domainEvent">The event to raise.</param>
    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
