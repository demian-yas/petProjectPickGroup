using PickGroup.Domain.Common;

namespace PickGroup.Domain.Picking.PickingsIds;

public sealed record PickingTaskId : MethodsId
{
    private PickingTaskId(Guid id) : base(id) { }
    
    
    public static PickingTaskId NewPickingTaskId() => new(Guid.NewGuid());
    
    public static PickingTaskId Empty() => new(Guid.Empty);

    public static PickingTaskId Create(Guid id) => new(id);
}