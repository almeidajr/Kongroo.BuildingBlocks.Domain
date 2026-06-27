# Kongroo.BuildingBlocks.Domain

DDD tactical building blocks — `Entity`, `AggregateRoot`, domain events, strongly-typed ids, and domain exceptions — shared across Kongroo microservice domain layers. Dependency-free and AOT-compatible.

## Install

```bash
dotnet add package Kongroo.BuildingBlocks.Domain
```

## Usage

```csharp
using Kongroo.BuildingBlocks.Domain;
using Kongroo.BuildingBlocks.Domain.Exceptions;

// Strongly-typed id (Guid-backed gets Create() for free):
public readonly record struct OrderId(Guid Value) : IGuidId<OrderId>
{
    public static OrderId From(Guid value) => new(value);
    public static OrderId Create() => From(Guid.CreateVersion7());
}

// Domain event with standard metadata (EventId + OccurredAt):
public sealed record OrderPlaced(OrderId OrderId) : DomainEvent;

// Aggregate root: identity equality + buffered domain events:
public sealed class Order : AggregateRoot<OrderId>
{
    public static Order Place() => new() { Id = OrderId.Create() };

    public void Confirm()
    {
        if (DomainEvents.Count > 0)
        {
            throw new ConflictException(nameof(Order), "already placed");
        }

        RaiseDomainEvent(new OrderPlaced(Id));
    }
}
```

Infrastructure can dispatch buffered events by detecting `IHasDomainEvents`, and wire
generic id converters via `IStronglyTypedId<TSelf, TValue>.From`.
