namespace PickGroup.Domain.Orders;

public enum OrderStatus
{
    Pending, // Заказ ожидает подготовки к сборке
    ReadyForPicking, // Заказ готов к передаче в складской процесс
    InProgress, // Заказ обрабатывается на складе
    Fulfilled, // Заказ отгружен
    Cancelled // Заказ отменен
}