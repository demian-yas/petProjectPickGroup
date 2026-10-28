using CSharpFunctionalExtensions;
using PickGroup.Domain.Common;
using PickGroup.Domain.Orders.OrdersIds;

namespace PickGroup.Domain.Orders;

public class OrderItem : Common.Entity<OrderItemId>
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
        Unit unit) : base(orderItemId)
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
    public Unit Unit { get; private set; }

    public static Result<OrderItem, Error> Create(
        OrderItemId orderItemId,
        string? externalId,
        string storageLocationCode,
        string productName,
        bool requiresManufactureDate,
        string? comment,
        decimal requiredQuantity,
        Unit unit)
    {
        if (orderItemId is null)
            return Errors.General.ValueIsEmpty("orderItemId");
        
        if (string.IsNullOrWhiteSpace(storageLocationCode))
            return Errors.General.ValueIsEmpty("storageLocationCode");
        
        if (string.IsNullOrWhiteSpace(productName))
            return Errors.General.ValueIsEmpty("productName");
        
        if (requiredQuantity <= 0)
            return Errors.General.ValueIsInvalid("requiredQuantity");
        
        var orderItem = new OrderItem(
            orderItemId,
            externalId,
            storageLocationCode,
            productName,
            requiresManufactureDate,
            comment,
            requiredQuantity,
            unit);

        return orderItem;
    }
}