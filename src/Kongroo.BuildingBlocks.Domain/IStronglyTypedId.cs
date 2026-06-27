namespace Kongroo.BuildingBlocks.Domain;

/// <summary>Marker for strongly-typed identifiers; implement a generic variant, not this directly.</summary>
public interface IStronglyTypedId;

/// <summary>
/// A strongly-typed identifier wrapping a primitive <typeparamref name="TValue"/>, with an
/// enforced <see cref="From"/> factory.
/// </summary>
/// <typeparam name="TSelf">The implementing id type.</typeparam>
/// <typeparam name="TValue">The underlying primitive type (e.g. Guid, long, string).</typeparam>
public interface IStronglyTypedId<out TSelf, TValue> : IStronglyTypedId
    where TSelf : IStronglyTypedId<TSelf, TValue>
    where TValue : IEquatable<TValue>
{
    /// <summary>The underlying primitive value.</summary>
    public TValue Value { get; }

    /// <summary>Creates an id wrapping <paramref name="value"/>.</summary>
    public static abstract TSelf From(TValue value);
}

/// <summary>A Guid-backed strongly-typed id with an enforced <see cref="Create"/> factory.</summary>
/// <typeparam name="TSelf">The implementing id type.</typeparam>
public interface IGuidId<out TSelf> : IStronglyTypedId<TSelf, Guid>
    where TSelf : IGuidId<TSelf>
{
    /// <summary>Creates a new id with a fresh UUIDv7 value.</summary>
    public static abstract TSelf Create();
}
