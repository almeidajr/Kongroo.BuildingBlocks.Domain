using Shouldly;

namespace Kongroo.BuildingBlocks.Domain.Tests;

public sealed class EntityTests
{
    [Fact]
    public void Equals_WithSameTypeAndId_ShouldBeEqual()
    {
        var id = Guid.NewGuid();

        var a = CustomerWith(id);
        var b = CustomerWith(id);

        a.ShouldBe(b);
        (a == b).ShouldBeTrue();
        (a != b).ShouldBeFalse();
        a.GetHashCode().ShouldBe(b.GetHashCode());
    }

    [Fact]
    public void Equals_WithDifferentIds_ShouldNotBeEqual()
    {
        var a = CustomerWith(Guid.NewGuid());
        var b = CustomerWith(Guid.NewGuid());

        a.ShouldNotBe(b);
        (a == b).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WithDifferentConcreteTypesButSameId_ShouldNotBeEqual()
    {
        var id = OrderId.From(Guid.NewGuid());

        Entity<OrderId> customer = new Customer { Id = id };
        Entity<OrderId> product = new Product { Id = id };

        customer.Equals(product).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WhenComparedToNull_ShouldNotBeEqual()
    {
        var a = CustomerWith(Guid.NewGuid());

        a.Equals(null).ShouldBeFalse();
        (a == null).ShouldBeFalse();
        (null == a).ShouldBeFalse();
    }

    private static Customer CustomerWith(Guid id) => new() { Id = OrderId.From(id) };

    private sealed class Customer : Entity<OrderId>;

    private sealed class Product : Entity<OrderId>;
}
