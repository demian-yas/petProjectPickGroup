namespace PickGroup.Domain.Orders;

public class Order
{
    private readonly List<OrderItem> _items = [];

    // ef core
    private Order() { }
    
    
    // Внутренний ID заказа
    public Guid Id { get; private set; }
    
    // Номер, который видят сотрудники
    public string OrderNumber { get; private set; } = null!;
    
    // ID заказа во внешней системе, например 1С
    public string? ExternalId { get; private set; }

    // Имя человека, который принял заказ
    public string PersonNameAccepted { get; private set; } = null!;
    
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
    
    // Указананое время для отправки отправки
    // Можно сделать VO !!!
    public string DispatchTime { get; private set; } = null!;
    
    // Время создания
    public DateTime CreatedAt { get; private set; }
    
    // Время последнего изменения
    public DateTime UpdatedAt { get; private set; }
}