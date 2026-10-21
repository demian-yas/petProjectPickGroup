using System.Globalization;
using CSharpFunctionalExtensions;
using PickGroup.Domain.Common;

namespace PickGroup.Domain.Orders.OrderValueObjects;

public sealed record DeliverySchedule
{
     private DeliverySchedule(
          DateTime startTime,
          DateTime endTime,
          DeliveryShift shift)
     {
          StartTime = startTime;
          EndTime = endTime;
          Shift = shift;
     }
     
     private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
     private const int StartNightShift = 22;
     private const int EndNightShift = 6;
     
     public DateTime StartTime { get; }
     public DateTime EndTime { get; }
     public DeliveryShift Shift { get; }

     public static Result<DeliverySchedule, Error> Create(string startTimeStr, string endTimeStr)
     {
          if (string.IsNullOrWhiteSpace(startTimeStr))
               return Errors.General.ValueIsEmpty("startTimeStr");
          
          if (string.IsNullOrWhiteSpace(endTimeStr))
               return Errors.General.ValueIsEmpty("endTimeStr");

          if (!DateTime.TryParseExact(
                   startTimeStr,
                   DateTimeFormat,
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out var startTime))
          {
               return Errors.General.ValueIsInvalid(startTimeStr, "startTime");
          }

          if (!DateTime.TryParseExact(endTimeStr,
                   DateTimeFormat,
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out var endTime))
          {
               return Errors.General.ValueIsInvalid(endTimeStr, "endTime");
          }

          if (endTime <= startTime)
          {
               return Errors.General.InvalidTimeRange(startTimeStr, endTimeStr, "endTimeStr");
          }
          
          var shift = startTime.Hour >= StartNightShift || startTime.Hour < EndNightShift
               ? DeliveryShift.Day
               : DeliveryShift.Night;
          
          var deliverySchedule = new DeliverySchedule(startTime, endTime, shift);

          return deliverySchedule;
     }
     
}

public enum DeliveryShift
{
     Day,
     Night
}