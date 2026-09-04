namespace DirectoryService.Domain.Communications.Ids;

public class DepartmentPositionId
{
    private DepartmentPositionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static DepartmentPositionId NewDepartmentPositionId() => new(Guid.NewGuid());

    public static DepartmentPositionId Empty() => new(Guid.Empty);

    public static DepartmentPositionId Create(Guid id) => new(id);

    public static implicit operator DepartmentPositionId(Guid id) => new DepartmentPositionId(id);

    public static implicit operator Guid(DepartmentPositionId departmentPositionId)
    {
        ArgumentNullException.ThrowIfNull(departmentPositionId);

        return departmentPositionId.Value;
    }

    public static DepartmentPositionId FromGuid(Guid id) => new(id);

    public static Guid ToGuid(DepartmentPositionId departmentPositionId)
    {
        ArgumentNullException.ThrowIfNull(departmentPositionId);
        return departmentPositionId.Value;
    }
}