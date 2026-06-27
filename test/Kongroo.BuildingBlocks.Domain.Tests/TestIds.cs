namespace Kongroo.BuildingBlocks.Domain.Tests;

// Sample strongly-typed ids reused across Entity/AggregateRoot/Id tests.
// Internal to avoid CA1515 (public types in a non-API assembly).
internal readonly record struct OrderId(Guid Value) : IGuidId<OrderId>
{
    public static OrderId From(Guid value) => new(value);

    // required IGuidId<OrderId>.Create implementation (static abstract — no inherited default)
    public static OrderId Create() => From(Guid.CreateVersion7());
}

internal readonly record struct LineId(long Value) : IStronglyTypedId<LineId, long>
{
    public static LineId From(long value) => new(value);
}
