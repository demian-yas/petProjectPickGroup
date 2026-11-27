namespace PickGroup.OrderService.Domain.Loading;

public enum LoadingDecision
{
    Accepted, // Водитель принял заказ
    DiscrepancyFound // Водитель обнаружил расхождения
}