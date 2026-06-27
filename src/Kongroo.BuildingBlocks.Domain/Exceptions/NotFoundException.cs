namespace Kongroo.BuildingBlocks.Domain.Exceptions;

/// <summary>Raised when a requested resource cannot be found.</summary>
public sealed class NotFoundException : DomainException
{
    /// <summary>Initializes the exception for the given resource and lookup.</summary>
    public NotFoundException(string resourceName, string lookup)
        : base($"{resourceName} resource with {lookup} was not found.")
    {
        ResourceName = resourceName;
        Lookup = lookup;
    }

    /// <summary>Initializes the exception with an inner exception.</summary>
    public NotFoundException(string resourceName, string lookup, Exception innerException)
        : base($"{resourceName} resource with {lookup} was not found.", innerException)
    {
        ResourceName = resourceName;
        Lookup = lookup;
    }

    /// <summary>The name of the resource that was not found.</summary>
    public string ResourceName { get; }

    /// <summary>The lookup (e.g. id or key) that yielded no result.</summary>
    public string Lookup { get; }
}
