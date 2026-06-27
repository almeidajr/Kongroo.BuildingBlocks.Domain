using Shouldly;

namespace Kongroo.BuildingBlocks.Domain.Tests;

public sealed class StronglyTypedIdTests
{
    [Fact]
    public void From_WhenGivenValue_ShouldWrapIt()
    {
        var value = Guid.NewGuid();

        var id = OrderId.From(value);

        id.Value.ShouldBe(value);
    }

    [Fact]
    public void From_WhenCalledThroughGenericConstraint_ShouldConstructTheConcreteId()
    {
        var value = Guid.NewGuid();

        var id = MakeFrom<OrderId, Guid>(value);

        id.ShouldBe(new OrderId(value));
    }

    [Fact]
    public void From_WithNonGuidUnderlyingValue_ShouldWrapTheValue()
    {
        var id = MakeFrom<LineId, long>(42L);

        id.Value.ShouldBe(42L);
    }

    [Fact]
    public void Create_WhenCalledTwice_ShouldProduceDistinctIds()
    {
        var first = OrderId.Create();
        var second = MakeNew<OrderId>();

        first.ShouldNotBe(second);
        first.Value.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public void From_WhenIdsWrapTheSameValue_ShouldBeValueEqualAndImplementTheMarker()
    {
        var value = Guid.NewGuid();

        var a = OrderId.From(value);
        var b = OrderId.From(value);

        a.ShouldBeAssignableTo<IStronglyTypedId>();
        a.ShouldBe(b); // record value equality
    }

    // Proves the static factory is reachable only through a constrained type parameter.
    private static TId MakeFrom<TId, TValue>(TValue value)
        where TId : IStronglyTypedId<TId, TValue>
        where TValue : IEquatable<TValue> => TId.From(value);

    private static TId MakeNew<TId>()
        where TId : IGuidId<TId> => TId.Create();
}
