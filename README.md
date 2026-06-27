# 🦘 Kongroo.BuildingBlocks.Domain

[![CI](https://github.com/almeidajr/Kongroo.BuildingBlocks.Domain/actions/workflows/ci.yml/badge.svg)](https://github.com/almeidajr/Kongroo.BuildingBlocks.Domain/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Kongroo.BuildingBlocks.Domain.svg)](https://www.nuget.org/packages/Kongroo.BuildingBlocks.Domain)
[![Downloads](https://img.shields.io/nuget/dt/Kongroo.BuildingBlocks.Domain.svg)](https://www.nuget.org/packages/Kongroo.BuildingBlocks.Domain)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/almeidajr/Kongroo.BuildingBlocks.Domain/blob/main/LICENSE)

DDD tactical building blocks for the domain layer of my .NET microservices — entities, aggregate roots,
domain events, strongly-typed ids, and a domain-exception hierarchy. Dependency-free (BCL only) and
AOT-compatible, so it stays at the center of a clean-architecture dependency graph.

## Installation

```bash
dotnet add package Kongroo.BuildingBlocks.Domain
```

## Building blocks

| Type                                                                                    | Purpose                                                                           |
| --------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| `Entity<TId>`                                                                           | Identity-equality base with a `required init`, strongly-typed `Id`                |
| `AggregateRoot<TId>`                                                                    | Consistency boundary that buffers domain events (`[NotMapped]`)                   |
| `IHasDomainEvents`                                                                      | Hook for infrastructure to collect buffered events (e.g. an outbox)               |
| `IDomainEvent`                                                                          | Marker for domain events                                                          |
| `DomainEvent`                                                                           | Optional event base record with a UUIDv7 `EventId` and UTC `OccurredAt`           |
| `IStronglyTypedId` / `IStronglyTypedId<TSelf, TValue>`                                  | Typed-id markers enforcing a static `From` factory                                |
| `IGuidId<TSelf>`                                                                        | Guid-backed typed id, additionally enforcing a static `Create` factory            |
| `DomainException` (+ `ConflictException`, `NotFoundException`, `UnauthorizedException`) | Domain error channel; map to HTTP/problem-details in your API's exception handler |

## Usage

```csharp
using Kongroo.BuildingBlocks.Domain;
using Kongroo.BuildingBlocks.Domain.Exceptions;

// Strongly-typed id — implement the two one-line factories:
public readonly record struct OrderId(Guid Value) : IGuidId<OrderId>
{
    public static OrderId From(Guid value) => new(value);
    public static OrderId Create() => From(Guid.CreateVersion7());
}

// Domain event carrying standard metadata (EventId + OccurredAt):
public sealed record OrderPlaced(OrderId OrderId) : DomainEvent;

// Aggregate root — identity equality plus a buffered domain-event stream:
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

After persisting an aggregate, infrastructure detects `IHasDomainEvents` to dispatch its buffered
events and then call `ClearDomainEvents()`. Strongly-typed ids carry no converters of their own — wire
generic JSON / EF Core converters off `IStronglyTypedId<TSelf, TValue>` and its `TSelf.From(value)`
factory.

## Design notes

- **Value objects** are plain C# `record` / `readonly record struct` types — records already give
  value equality, so the kernel ships no `ValueObject` base.
- **Errors** are exceptions, not a `Result<T>` type: throw a `DomainException` and let the API layer
  translate it.
- **Identity** is exact-type: two entities are equal only when their concrete type _and_ `Id` match.
- **Strongly-typed ids** use static-abstract interface members (the `INumber<TSelf>` mechanism), so the
  `From` / `Create` factories are reachable from generic infrastructure with no reflection.

## What's baked in

- **Target**: `net10.0`, nullable + implicit usings, warnings-as-errors, latest .NET analyzers +
  SonarAnalyzer.
- **Zero dependencies**: BCL only, `IsAotCompatible`.
- **API stability**: the public surface is tracked with the public-API analyzer.
- **Packaging**: deterministic build, SourceLink, symbols (`snupkg`), and a MinVer-derived version.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download) or later.

## License

[MIT](https://github.com/almeidajr/Kongroo.BuildingBlocks.Domain/blob/main/LICENSE)
