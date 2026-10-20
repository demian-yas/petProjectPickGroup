using System.Globalization;
using CSharpFunctionalExtensions;

namespace PickGroup.Domain.Orders.OrderValueObjects;

public sealed record DispatchTime
{
     private DispatchTime(
          TimeOnly startTime, 
          TimeOnly endTime)
     {
          StartTime = startTime;
          EndTime = endTime;
     }
     
     
     public TimeOnly StartTime { get; }
     public TimeOnly EndTime { get; }

     public static Result<DispatchTime> Create(string startTimeStr, string endTimeStr)
     {
          if (!TimeOnly.TryParseExact(
                   startTimeStr,
                   "HH:mm",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out var startTime))
          {
               return Result.Failure<DispatchTime>("Invalid time format");
          }

          if (!TimeOnly.TryParseExact(
                   endTimeStr,
                   "HH:mm",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out var endTime))
          {
               return Result.Failure<DispatchTime>("Invalid time format");
          }

          var dispatchTime = new DispatchTime(startTime, endTime);

          return Result.Success(dispatchTime);
     }
}