using Kongroo.BuildingBlocks.Domain.Exceptions;
using Shouldly;

namespace Kongroo.BuildingBlocks.Domain.Tests;

public sealed class DomainExceptionTests
{
    [Fact]
    public void ConflictException_WhenConstructed_ShouldComposeMessageAndExposeProperties()
    {
        var exception = new ConflictException("Order", "already shipped");

        exception.Message.ShouldBe("Order resource conflict: already shipped.");
        exception.ResourceName.ShouldBe("Order");
        exception.Reason.ShouldBe("already shipped");
    }

    [Fact]
    public void NotFoundException_WhenConstructed_ShouldComposeMessageAndExposeProperties()
    {
        var exception = new NotFoundException("Order", "id 42");

        exception.Message.ShouldBe("Order resource with id 42 was not found.");
        exception.ResourceName.ShouldBe("Order");
        exception.Lookup.ShouldBe("id 42");
    }

    [Fact]
    public void UnauthorizedException_WhenConstructed_ShouldComposeMessageAndExposeProperties()
    {
        var exception = new UnauthorizedException("Order", "not the owner");

        exception.Message.ShouldBe("Order resource unauthorized: not the owner.");
        exception.ResourceName.ShouldBe("Order");
        exception.Reason.ShouldBe("not the owner");
    }

    [Fact]
    public void ConflictException_WhenConstructed_ShouldBeAssignableToDomainExceptionAndInvalidOperationException()
    {
        var exception = new ConflictException("Order", "x");

        exception.ShouldBeAssignableTo<DomainException>();
        exception.ShouldBeAssignableTo<InvalidOperationException>();
    }

    [Fact]
    public void Constructor_WhenGivenInnerException_ShouldPreserveIt()
    {
        var inner = new InvalidOperationException("root");

        var exception = new NotFoundException("Order", "id 1", inner);

        exception.InnerException.ShouldBeSameAs(inner);
    }
}
