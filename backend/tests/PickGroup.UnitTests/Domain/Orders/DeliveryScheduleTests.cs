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
    public void Create_ParamsAreIncorrectFormat_ShouldReturnFailure(string? startTimeStr, string? endTimeStr,
        string fieldName)
    {
        var result = DeliverySchedule.Create(startTimeStr!, endTimeStr!);
        
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
    
    
}