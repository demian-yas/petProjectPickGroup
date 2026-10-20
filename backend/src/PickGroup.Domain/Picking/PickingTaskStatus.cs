namespace PickGroup.Domain.Picking;

public enum PickingTaskStatus
{
    NotStarted, // Задание назначено, но еще не начато
    InProgress, // Сборка выполняется
    AwaitingHandover, // Сборка завершена, ожидается обработка
    ReadyForLoading, // Задание готово к передаче на погрузку
    Loading, // Водитель проверяет заказ, идет процесс погрузки
    Loaded, // Заказ загружен в транспорт 
    Dispatched, // Транспорт, отправился
    NeedsCorrection, // Обнаружена проблема
    Cancelled // задание отменено
}