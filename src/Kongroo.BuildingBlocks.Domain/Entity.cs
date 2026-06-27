using System.Diagnostics.CodeAnalysis;

namespace Kongroo.BuildingBlocks.Domain;

/// <summary>
/// Base type for domain entities. Entities are compared by identity, not by value.
/// </summary>
/// <typeparam name="TId">The strongly-typed identifier.</typeparam>
[SuppressMessage(
    "Minor Code Smell",
    "S4035:Classes implementing \"IEquatable<T>\" should be sealed",
    Justification = "Abstract identity base; equality is GetType()-exact and consistent across the hierarchy, so it is safe unsealed."
)]
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : IStronglyTypedId, IEquatable<TId>
{
    /// <summary>Gets the entity's unique identifier.</summary>
    public required TId Id { get; init; }

    /// <inheritdoc />
    public bool Equals(Entity<TId>? other) => other is not null && GetType() == other.GetType() && Id.Equals(other.Id);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Entity<TId> other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(GetType(), Id);

    /// <summary>Determines whether two entities have the same identity.</summary>
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => Equals(left, right);

    /// <summary>Determines whether two entities have different identities.</summary>
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !Equals(left, right);
}
