namespace BLL.Exceptions;

public class InvalidDateException : Exception
{
    public InvalidDateException()
        : base("Invalid date was provided.") { }

    public InvalidDateException(string fieldName)
        : base($"Invalid date was provided for {fieldName}.") { }
}
