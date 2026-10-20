namespace PickGroup.Domain.Loading;

public class LoadingResult
{
    // Идентификатор результата проверки
    public Guid Id { get; private set; }
    
    // Задание, к которому относится проверка
    public Guid PickingTaskId { get; private set; }
    
    // Идентификатор водителя
    public Guid DriverId { get; private set; }
    
    // Итог проверки
    public LoadingDecision Decision { get; private set; }
    
    // Описание расхождения или комментарий
    public string? Comment { get; private set; }
    
    // Когда водитель выполнил проверку
    public DateTime CheckedAt { get; private set; }
}