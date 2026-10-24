using PickGroup.Domain.Common;
using PickGroup.Domain.Picking.PickingsIds;

namespace PickGroup.Domain.Picking;

public class PickingTaskItem : Entity<PickingTaskItemId>
{
    // ef core
    private PickingTaskItem(PickingTaskItemId id) : base(id) { }

    private PickingTaskItem(
        PickingTaskItemId pickingTaskItemId,
        Guid orderItemId,
        string productCode,
        string productName,
        string storageLocationCode,
        decimal requiredQuantity,
        decimal pickedQuantity,
        string? shortageReason,
        DateOnly? manufactureDate) : base(pickingTaskItemId)
    {
        OrderItemId = orderItemId;
        ProductCode = productCode;
        ProductName = productName;
        StorageLocationCode = storageLocationCode;
        RequiredQuantity = requiredQuantity;
        PickedQuantity = pickedQuantity;
        ShortageReason = shortageReason;
        ManufactureDate = manufactureDate;
    }
    
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