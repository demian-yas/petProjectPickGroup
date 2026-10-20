using PickGroup.Domain.Common;
using PickGroup.Domain.Orders.OrdersIds;

namespace PickGroup.Domain.Orders;

public class Order : Entity<OrderId>
{
    private readonly List<OrderItem> _items = [];

    // ef core
    private Order(OrderId id) : base(id) { }

    private Order(
        OrderId orderId, 
        string orderNumber,
        string? externalId,
        string managerName,
        string managerPhoneNumber,
        string deliveryAddress,
        string dispatchTime) : base(orderId)
    {
        OrderNumber = orderNumber;
        ExternalId = externalId;
        ManagerName = managerName;
        ManagerPhoneNumber = managerPhoneNumber;
        DeliveryAddress = deliveryAddress;
        DispatchTime = dispatchTime;
        CreatedAt = DateTime.Now;
    }
    
    // Номер, который видят сотрудники
    public string OrderNumber { get; private set; } = null!;
    
    // ID заказа во внешней системе, например 1С
    public string? ExternalId { get; private set; }

    // Имя человека, который принял заказ
    public string ManagerName { get; private set; } = null!;
    
    // Номер телефона менеджера
    // Можно сделать VO !!!
    public string ManagerPhoneNumber { get; private set; } = null!;
    
    // Позиции заказа
    public IReadOnlyCollection<OrderItem> Items => _items;

    // Текущее состояние заказа
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    
    // Версия состава заказа
    public int Version { get; private set; } = 1;
    
    // Адрес доставки
    public string DeliveryAddress { get; private set; } = null!;
    
    // Указанное время для отправки
    // Можно сделать VO !!!
    public string DispatchTime { get; private set; } = null!;
    
    // Время создания
    public DateTime CreatedAt { get; private set; }
    
    // Время последнего изменения
    public DateTime UpdatedAt { get; private set; }
}