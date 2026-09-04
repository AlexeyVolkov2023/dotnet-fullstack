namespace DirectoryService.Domain.Communications.Ids;

public class DepartmentLocationId
{
    private DepartmentLocationId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static DepartmentLocationId NewDepartmentLocationId() => new(Guid.NewGuid());

    public static DepartmentLocationId Empty() => new(Guid.Empty);

    public static DepartmentLocationId Create(Guid id) => new(id);

    public static implicit operator DepartmentLocationId(Guid id) => new DepartmentLocationId(id);

    public static implicit operator Guid(DepartmentLocationId departmentLocationId)
    {
        ArgumentNullException.ThrowIfNull(departmentLocationId);

        return departmentLocationId.Value;
    }
    
    public static DepartmentLocationId FromGuid(Guid id) => new(id);

    public static Guid ToGuid(DepartmentLocationId departmentLocationId)
    {
        ArgumentNullException.ThrowIfNull(departmentLocationId);
        return departmentLocationId.Value;
    }
}