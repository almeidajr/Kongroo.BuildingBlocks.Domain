using Shouldly;

namespace Kongroo.BuildingBlocks.Domain.Tests;

public sealed class DomainEventTests
{
    [Fact]
    public void Constructor_WhenEventIsCreated_ShouldPopulateNonEmptyEventIdAndUtcOccurredAt()
    {
        var before = DateTimeOffset.UtcNow;

        var domainEvent = new SampleEvent("created");

        domainEvent.EventId.ShouldNotBe(Guid.Empty);
        domainEvent.OccurredAt.ShouldBeGreaterThanOrEqualTo(before);
        domainEvent.OccurredAt.Offset.ShouldBe(TimeSpan.Zero);
    }

    [Fact]
    public void EventId_WhenTwoEventsAreCreated_ShouldBeDistinct()
    {
        var first = new SampleEvent("a");
        var second = new SampleEvent("b");

        first.EventId.ShouldNotBe(second.EventId);
    }

    [Fact]
    public void With_WhenOverridingMetadata_ShouldReplaceEventIdAndOccurredAt()
    {
        var storedId = Guid.NewGuid();
        var storedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        var domainEvent = new SampleEvent("x") { EventId = storedId, OccurredAt = storedAt };
        var copy = domainEvent with { Name = "y" };

        copy.EventId.ShouldBe(storedId);
        copy.OccurredAt.ShouldBe(storedAt);
        copy.Name.ShouldBe("y");

        var newId = Guid.NewGuid();
        var newAt = new DateTimeOffset(2030, 6, 6, 0, 0, 0, TimeSpan.Zero);
        var rehydrated = domainEvent with { EventId = newId, OccurredAt = newAt };

        rehydrated.EventId.ShouldBe(newId);
        rehydrated.OccurredAt.ShouldBe(newAt);
    }

    [Fact]
    public void DomainEvent_WhenDerived_ShouldImplementIDomainEvent() =>
        new SampleEvent("z").ShouldBeAssignableTo<IDomainEvent>();

    private sealed record SampleEvent(string Name) : DomainEvent;
}
