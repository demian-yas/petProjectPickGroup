namespace PickGroup.Domain.Picking;

public class PickingTaskItem
{
    
    // Уникальный идентификатор
    public Guid Id { get; private set; }
    
    // Ссылка на исходную позицию заказа
    public Guid OrderItemId { get; private set; }

    // Артикул товара на момент создания задания
    public string ProductCode { get; private set; } = null!;

    // Название товара на момент создания задания
    public string ProductName { get; private set; } = null!;

    // Место хранения товара на момент создания задания
    public string StorageLocationCode { get; private set; } = null!;
    
    // Количество, которое нужно собрать
    public decimal RequiredQuantity { get; private set; }
    
    // Количество, которое фактически собрано
    public decimal PickedQuantity { get; private set; }
    
    // Причина, по которой собрано меньше
    public string? ShortageReason { get; private set; }
    
    // Дата производства, которую записал сборщик
    public DateOnly? ManufactureDate { get; private set; }
}