using PickGroup.OrderService.Domain.Common;

namespace PickGroup.OrderService.API.Response;

public record Envelope
{
    public object? Result { get; }
    public string? StatusCode { get; }
    public string? Message { get; }
    public DateTime? Timestamp { get; }

    private Envelope(
        object? result,
        Error? error)
    {
        Result = result;
        StatusCode = error?.Code;
        Message = error?.Message;
        Timestamp = DateTime.UtcNow;
    }
    
    public static Envelope Ok(object? result) => new Envelope(result, null);
    public static Envelope Error(Error? error) => new Envelope(null, error);
}