namespace Kongroo.BuildingBlocks.Domain.Exceptions;

/// <summary>Raised when an operation conflicts with the current state of a resource.</summary>
public sealed class ConflictException : DomainException
{
    /// <summary>Initializes the exception for the given resource and reason.</summary>
    public ConflictException(string resourceName, string reason)
        : base($"{resourceName} resource conflict: {reason}.")
    {
        ResourceName = resourceName;
        Reason = reason;
    }

    /// <summary>Initializes the exception with an inner exception.</summary>
    public ConflictException(string resourceName, string reason, Exception innerException)
        : base($"{resourceName} resource conflict: {reason}.", innerException)
    {
        ResourceName = resourceName;
        Reason = reason;
    }

    /// <summary>The name of the conflicting resource.</summary>
    public string ResourceName { get; }

    /// <summary>The reason for the conflict.</summary>
    public string Reason { get; }
}
