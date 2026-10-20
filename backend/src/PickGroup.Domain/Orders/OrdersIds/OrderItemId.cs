using PickGroup.Domain.Common;

namespace PickGroup.Domain.Orders.OrdersIds;

public sealed record OrderItemId : MethodsId
{
    private OrderItemId(Guid id) : base(id) { }

    public static OrderItemId NewOrderItemId() => new(Guid.NewGuid());
    
    public static OrderItemId Empty() => new(Guid.Empty);

    public static OrderItemId Create(Guid id) => new(id);
}