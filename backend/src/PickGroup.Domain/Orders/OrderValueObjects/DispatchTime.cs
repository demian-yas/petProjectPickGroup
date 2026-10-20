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
          TimeOnly startTime = TimeOnly.ParseExact(startTimeStr, "HH:mm", CultureInfo.InvariantCulture);
          TimeOnly endTime = TimeOnly.ParseExact(endTimeStr, "HH:mm", CultureInfo.InvariantCulture);

          var dispatchTime = new DispatchTime(startTime, endTime);

          return dispatchTime;
     }
}