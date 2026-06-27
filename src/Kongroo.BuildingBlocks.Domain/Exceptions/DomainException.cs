namespace Kongroo.BuildingBlocks.Domain.Exceptions;

/// <summary>Base for expected, domain-rule failures.</summary>
public abstract class DomainException : InvalidOperationException
{
    /// <summary>Initializes the exception with a message.</summary>
    protected DomainException(string message)
        : base(message) { }

    /// <summary>Initializes the exception with a message and an inner exception.</summary>
    protected DomainException(string message, Exception innerException)
        : base(message, innerException) { }
}
