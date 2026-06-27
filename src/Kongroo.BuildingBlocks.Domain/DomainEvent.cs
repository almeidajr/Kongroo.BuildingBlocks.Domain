namespace Kongroo.BuildingBlocks.Domain;

/// <summary>
/// Optional base record for domain events, supplying a generated <see cref="EventId"/> and
/// <see cref="OccurredAt"/>; implement <see cref="IDomainEvent"/> directly to opt out.
/// </summary>
public abstract record DomainEvent : IDomainEvent
{
    /// <summary>A unique, time-ordered (UUIDv7) identifier for this event.</summary>
    public Guid EventId { get; init; } = Guid.CreateVersion7();

    /// <summary>The instant the event occurred, in UTC.</summary>
    public DateTimeOffset OccurredAt { get; init; } = DateTimeOffset.UtcNow;
}
