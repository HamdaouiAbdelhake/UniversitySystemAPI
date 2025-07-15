namespace University.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Record Not Found")
    {
    }

    public NotFoundException(string? message) : base(message)
    {
    }
}