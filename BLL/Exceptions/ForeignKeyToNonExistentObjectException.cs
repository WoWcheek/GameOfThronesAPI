namespace BLL.Exceptions;

public class ForeignKeyToNonExistentObjectException : Exception
{
    public ForeignKeyToNonExistentObjectException() 
        : base("Foreign key points to non-existent object.") { }

    public ForeignKeyToNonExistentObjectException(string fieldName)
        : base($"{fieldName} points to non-existent object.") { }

    public ForeignKeyToNonExistentObjectException(string fieldName, string objectName)
        : base($"{fieldName} points to non-existent {objectName}.") { }
}
