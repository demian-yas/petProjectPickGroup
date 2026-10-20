namespace PickGroup.Domain.Employees;

public class Driver
{
    
    // Внутренний идентификатор водителя
    public Guid Id { get; private set; }
    
    // Табельный номер
    public string EmployeeNumber { get; private set; } = null!;
    
    // Имя или отображаемое имя 
    public string DisplayName { get; private set; } = null!;
    
    // Может ли водитель выполнять проверки
    public bool IsActive { get; private set; }
}