namespace Kongroo.BuildingBlocks.Domain.Exceptions;

/// <summary>Raised when an operation on a resource is not permitted.</summary>
public sealed class UnauthorizedException : DomainException
{
    /// <summary>Initializes the exception for the given resource and reason.</summary>
    public UnauthorizedException(string resourceName, string reason)
        : base($"{resourceName} resource unauthorized: {reason}.")
    {
        ResourceName = resourceName;
        Reason = reason;
    }

    /// <summary>Initializes the exception with an inner exception.</summary>
    public UnauthorizedException(string resourceName, string reason, Exception innerException)
        : base($"{resourceName} resource unauthorized: {reason}.", innerException)
    {
        ResourceName = resourceName;
        Reason = reason;
    }

    /// <summary>The name of the resource.</summary>
    public string ResourceName { get; }

    /// <summary>The reason authorization failed.</summary>
    public string Reason { get; }
}
