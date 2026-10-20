namespace PickGroup.Domain.Employees;

public class Picker
{
    // Внутренний идентификатор сотрудника
    public Guid Id { get; private set; }

    // Табельный номер сотрудника
    public string EmployeeNumber { get; private set; } = null!;
    
    // Имя или отображаемое имя сотрудника
    public string DisplayName { get; private set; } = null!;
    
    // Может ли сотрудник получать новые задания
    public bool IsActive { get; private set; }
}