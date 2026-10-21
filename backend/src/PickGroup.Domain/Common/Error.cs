namespace PickGroup.Domain.Common;

public record Error
{
    private Error(
        string code, 
        string message,
        string? field,
        ErrorType errorType)
    {
        Code = code;
        Message = message;
        Field = field;
        ErrorType = errorType;
    }
    
    public string Code { get; }
    public string Message { get; }
    public string? Field { get; }
    public ErrorType ErrorType { get; }
    
    public static Error Validation(string code, string message, string? field = null)
        => new Error(code, message, field, ErrorType.Validation);
    
    public static Error NotFound(string code, string message, string? field = null)
        => new Error(code, message, field, ErrorType.NotFound);
    
    public static Error Failure(string code, string message, string? field = null)
        => new Error(code, message, field, ErrorType.Failure);
    
    public static Error Conflict(string code, string message, string? field = null)
        => new Error(code, message, field, ErrorType.Conflict);
}

public enum ErrorType
{
    Validation,
    NotFound,
    Failure,
    Conflict
}