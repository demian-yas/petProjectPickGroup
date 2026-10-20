using PickGroup.Domain.Common;

namespace PickGroup.Domain.Orders.OrdersIds;

public sealed record OrderId : MethodsId
{
    private OrderId(Guid value) : base(value) { }
    
    public static OrderId NewOrderId() => new(Guid.NewGuid());
    
    public static OrderId Empty() => new(Guid.Empty);

    public static OrderId Create(Guid id) => new(id);
}