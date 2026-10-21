namespace PickGroup.Domain.Common;

public class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null, string? field = null)
        {
            var label = name ?? "value";

            return Error.Validation("value.is.invalid", $"{label} is invalid", $"field: {field}");
        }

        public static Error InvalidTimeRange(string startTime, string endTime, string? field = null)
        {
            return Error.Validation("value.is.invalid", $"{startTime} must be earlier than {endTime}", $"field: {field}");
        }

        public static Error ValueIsEmpty(string? name = null)
        {
            var label = name ?? "input field";
            
            return Error.Validation("value.is.empty", $"{label} is empty");
        }

        public static Error NotFound(Guid? id = null, string? field = null)
        {
            var forId = id == null ? "" : $" for Id: {id} ";

            return Error.NotFound("record.not.found", $"record not found {forId}", $"field: {field}");
        }
    }
}