using PickGroup.Domain.Common;
using PickGroup.Domain.Orders.OrderValueObjects;

namespace PickGroup.UnitTests.Domain.Orders;

public class DeliveryScheduleTests
{
    [Theory]
    [InlineData(null, "2026-11-18 15:00:00", "startTimeStr")]
    [InlineData("2026-11-18 14:00:00", null, "endTimeStr")]
    [InlineData("", "2026-11-18 15:00:00", "startTimeStr")]
    [InlineData("2026-11-18 14:00:00", "", "endTimeStr")]
    [InlineData(" ", "2026-11-18 15:00:00", "startTimeStr")]
    [InlineData("2026-11-18 14:00:00", " ", "endTimeStr")]
    [InlineData(null, null, "startTimeStr")]
    public void Create_WhenParamsAreNullOrWhiteSpace_ShouldReturnFailure(string? startTimeStr, string? endTimeStr, 
        string paramName)
    {
        var result = DeliverySchedule.Create(startTimeStr!, endTimeStr!);
        
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.General.ValueIsEmpty(paramName), result.Error);
    }

    [Theory]
    [InlineData("11-18-2026", "2026-11-18 15:00:00", "startTime")]
    [InlineData("2026-11-18 14:00:00", "11-18-2026 15:00:00", "endTime")]
    [InlineData("2026-11-18 50:00:00", "2026-11-18 15:00:00", "startTime")]
    [InlineData("2026-11-18 14:00:00", "2026-11-18 50:00:00", "endTime")]
    [InlineData("2026-11-18 50:00:00", "2026-11-18 50:00:00", "startTime")]
    public void Create_ParamsAreIncorrectFormat_ShouldReturnFailure(string startTimeStr, string endTimeStr,
        string fieldName)
    {
        var result = DeliverySchedule.Create(startTimeStr, endTimeStr);
        
        Assert.True(result.IsFailure);

        if (fieldName == "startTime")
        {
            Assert.Equal(Errors.General.ValueIsInvalid(startTimeStr, fieldName), result.Error);
        }
        else
        {
            Assert.Equal(Errors.General.ValueIsInvalid(endTimeStr, fieldName), result.Error);
        }
    }

    [Theory]
    [InlineData("2026-11-18 15:00:00", "2026-11-18 15:00:00", "endTimeStr")]
    [InlineData("2026-11-18 16:00:00", "2026-11-18 15:00:00", "endTimeStr")]
    public void Create_WhenInvalidTimeRange_ShouldReturnFailure(string startTimeStr, string endTimeStr, string paramName)
    {
        var result = DeliverySchedule.Create(startTimeStr, endTimeStr);
        
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.General.InvalidTimeRange(startTimeStr, endTimeStr, paramName), result.Error);
    }

    [Theory]
    [InlineData("2026-11-18 15:00:00", "2026-11-18 16:00:00")]
    [InlineData("2026-11-18 23:00:00", "2026-11-19 01:00:00")]
    public void Create_WhenArgumentAreValid_CreateDeliverySchedule(string startTimeStr, string endTimeStr)
    {
        var result = DeliverySchedule.Create(startTimeStr, endTimeStr);
        
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("2026-11-18 22:00:00", "2026-11-19 08:00:00")]
    [InlineData("2026-11-18 23:00:00", "2026-11-19 08:00:00")]
    [InlineData("2026-11-19 05:59:59", "2026-11-19 08:00:00")]
    [InlineData("2026-11-19 02:00:00", "2026-11-19 08:00:00")]
    public void Create_WhenStartTimeIsNight_ShouldShiftIsNight(string startTimeStr, string endTimeStr)
    {
        var result = DeliverySchedule.Create(startTimeStr, endTimeStr);
        
        Assert.True(result.IsSuccess);
        Assert.Equal(DeliveryShift.Night, result.Value.Shift);
    }

    [Theory]
    [InlineData("2026-11-19 06:00:00", "2026-11-19 08:00:00")]
    [InlineData("2026-11-19 06:00:01", "2026-11-19 08:00:00")]
    [InlineData("2026-11-19 21:59:59", "2026-11-19 22:30:00")]
    [InlineData("2026-11-19 20:00:00", "2026-11-19 21:00:00")]
    public void Create_WhenStartTimeIsDay_ShouldShiftIsDay(string startTimeStr, string endTimeStr)
    {
        var result = DeliverySchedule.Create(startTimeStr, endTimeStr);
        
        Assert.True(result.IsSuccess);
        Assert.Equal(DeliveryShift.Day, result.Value.Shift);
    }

    [Fact]
    public void Create_WhenRangeCrossesMidnight_PreservesStartAndEndTimes()
    {
        var result = DeliverySchedule.Create("2026-11-18 23:00:00", "2026-11-19 01:00:00");

        Assert.True(result.IsSuccess);
        var schedule = result.Value;

        Assert.Equal(new DateTime(2026, 11, 18, 23, 0, 0), schedule.StartTime);
        Assert.Equal(new DateTime(2026, 11, 19, 1, 0, 0), schedule.EndTime);
        Assert.Equal(DeliveryShift.Night, schedule.Shift);
    }
}