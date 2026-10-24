
using PickGroup.Domain.Common;
using PickGroup.Domain.Picking.PickingsIds;

namespace PickGroup.Domain.Picking;

public class PickingTask : Entity<PickingTaskId>
{
    // ef core
    private PickingTask(PickingTaskId id) : base(id) { }

    private PickingTask(
        PickingTaskId pickingTaskId,
        Guid orderId,
        int orderVersion,
        Guid pickerId,
        PickingTaskStatus status,
        DateTime assignedAt,
        DateTime? startedAt,
        DateTime? pickingCompletedAt) : base(pickingTaskId)
    {
        OrderId = orderId;
        OrderVersion = orderVersion;
        PickerId = pickerId;
        Status = status;
        AssignedAt = assignedAt;
        StartedAt = startedAt;
        PickingCompletedAt = pickingCompletedAt;
    }
    
    // Ссылка на заказ
    public Guid OrderId { get; private set; }
    
    // Версия заказа, на основе которой создано задание
    public int OrderVersion { get; private set; }
    
    // Ссылка на сборщика
    public Guid PickerId { get; private set; }

    // Текущий этап выполнения задания
    public PickingTaskStatus Status { get; private set; } = PickingTaskStatus.NotStarted;
    
    // Когда задание назначили сборщику
    public DateTime AssignedAt { get; private set; }
    
    // Когда сборщик начал работу
    public DateTime? StartedAt { get; private set; }
    
    // Когда сборщик закончил сборку
    public DateTime? PickingCompletedAt { get; private set; }
}