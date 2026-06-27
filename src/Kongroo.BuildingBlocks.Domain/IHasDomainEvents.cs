namespace Kongroo.BuildingBlocks.Domain;

/// <summary>
/// Implemented by aggregates that buffer domain events, so infrastructure can collect them
/// generically (e.g. an EF Core <c>SaveChanges</c> interceptor feeding an outbox).
/// </summary>
public interface IHasDomainEvents
{
    /// <summary>The domain events buffered since load or last clear.</summary>
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }

    /// <summary>Clears the buffered events (typically after dispatch).</summary>
    public void ClearDomainEvents();
}
