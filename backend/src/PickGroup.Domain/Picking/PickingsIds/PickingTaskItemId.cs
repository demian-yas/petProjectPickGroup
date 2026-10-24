using PickGroup.Domain.Common;

namespace PickGroup.Domain.Picking.PickingsIds;

public sealed record PickingTaskItemId : MethodsId
{
    private PickingTaskItemId(Guid id) : base(id) { }
    
    public static PickingTaskItemId NewPickingItemId() => new(Guid.NewGuid());
    
    public static PickingTaskItemId Empty() => new(Guid.Empty);

    public static PickingTaskItemId Create(Guid id) => new(id);
}