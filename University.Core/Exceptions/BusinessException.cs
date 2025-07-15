namespace University.Core.Exceptions;

public class BusinessException : Exception
{
    public Dictionary<string, List<String>> Errors;

    public BusinessException(Dictionary<string, List<string>> errors)
    {
        Errors = errors ?? new Dictionary<string, List<string>>();
    }

    public BusinessException(string? message) : base(message)
    {
        Errors = new Dictionary<string, List<string>>();
    }
}