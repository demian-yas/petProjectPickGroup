
namespace PickGroup.Domain.Picking;

public class PickingTask
{
    // Уникальный идентификатор задания
    public Guid Id { get; private set; }
    
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