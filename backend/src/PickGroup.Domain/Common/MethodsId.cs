namespace PickGroup.Domain.Common;

public abstract class MethodsId
{
    protected MethodsId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; private set; }
}