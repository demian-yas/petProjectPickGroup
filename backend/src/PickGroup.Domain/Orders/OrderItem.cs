using PickGroup.Domain.Common;
using PickGroup.Domain.Orders.OrdersIds;

namespace PickGroup.Domain.Orders;

public class OrderItem : Entity<OrderItemId>
{
    // ef core
    private OrderItem(OrderItemId id) : base(id) { }

    private OrderItem(
        OrderItemId orderItemId,
        string? externalId,
        string storageLocationCode,
        string productName,
        bool requiresManufactureDate,
        string? comment,
        decimal requiredQuantity,
        string unit) : base(orderItemId)
    {
        ExternalId = externalId;
        StorageLocationCode = storageLocationCode;
        ProductName = productName;
        RequiresManufactureDate = requiresManufactureDate;
        Comment = comment;
        RequiredQuantity = requiredQuantity;
        Unit = unit;
    }
    
    // ID товара во внешней системе, например 1С
    public string? ExternalId { get; private set; }

    // Место хранения товара
    public string StorageLocationCode { get; private set; } = null!;
    
    // Наименование товара
    public string ProductName { get; private set; } = null!;
    
    // Наличие сроков товара
    public bool RequiresManufactureDate { get; private set; }
    
    // Комментарий к товару
    public string? Comment { get; private set; }
    
    // Количество товара, который захотел клиент
    public decimal RequiredQuantity { get; private set; }

    // базовая единица
    // VO
    public string Unit { get; private set; } = null!;
}