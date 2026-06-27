using NSubstitute;
using Shouldly;

namespace Kongroo.BuildingBlocks.Domain.Tests;

public sealed class AggregateRootTests
{
    [Fact]
    public void RaiseDomainEvent_WhenCalled_ShouldBufferEvent()
    {
        var aggregate = NewAggregate();

        aggregate.DoSomething();

        aggregate.DomainEvents.Count.ShouldBe(1);
        aggregate.DomainEvents[0].ShouldBeOfType<SampleRaised>();
    }

    [Fact]
    public void RaiseDomainEvent_WithSubstitutedEvent_ShouldBufferThatInstance()
    {
        var substituteEvent = Substitute.For<IDomainEvent>();
        var aggregate = NewAggregate();

        aggregate.Raise(substituteEvent);

        aggregate.DomainEvents.ShouldHaveSingleItem().ShouldBeSameAs(substituteEvent);
    }

    [Fact]
    public void ClearDomainEvents_WhenCalled_ShouldEmptyTheBuffer()
    {
        var aggregate = NewAggregate();
        aggregate.DoSomething();

        aggregate.ClearDomainEvents();

        aggregate.DomainEvents.ShouldBeEmpty();
    }

    [Fact]
    public void DomainEvents_WhenAccessedViaIHasDomainEvents_ShouldExposeBufferedEvents()
    {
        IHasDomainEvents aggregate = NewAggregate();
        ((SampleAggregate)aggregate).DoSomething();

        aggregate.DomainEvents.ShouldHaveSingleItem();
    }

    private static SampleAggregate NewAggregate() => new() { Id = OrderId.Create() };

    private sealed record SampleRaised : DomainEvent;

    private sealed class SampleAggregate : AggregateRoot<OrderId>
    {
        public void DoSomething() => RaiseDomainEvent(new SampleRaised());

        public void Raise(IDomainEvent domainEvent) => RaiseDomainEvent(domainEvent);
    }
}
