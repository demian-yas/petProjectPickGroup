namespace PickGroup.Domain.Common;

public class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";

            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }

        public static Error ValueIsEmpty(string? name = null)
        {
            var label = name ?? "input field";
            
            return Error.Validation("value.is.empty", $"{label} is empty");
        }

        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id: {id} ";

            return Error.NotFound("record.not.found", $"record not found {forId}");
        }
    }
}