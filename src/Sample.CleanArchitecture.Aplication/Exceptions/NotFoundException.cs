using System.Diagnostics.CodeAnalysis;

namespace Sample.CleanArchitecture.Application.Exceptions;

[ExcludeFromCodeCoverage]
public class NotFoundException : Exception
{
    public NotFoundException(string? message)
        : base(message)
    {
    }

    public static void ThrowIfNull(
        object? @object,
        string exceptionMessage)
    {
        if (@object == null)
            throw new NotFoundException(exceptionMessage);
    }
}