namespace PickGroup.Domain.Orders;

public class OrderItem
{
    // Внутренний ID заказа
    public Guid Id { get; private set; }
    
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
    public string Unit { get; private set; } = null!;
}