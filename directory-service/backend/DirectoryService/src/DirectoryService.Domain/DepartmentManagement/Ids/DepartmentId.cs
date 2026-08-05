namespace DirectoryService.Domain.DepartmentManagement.Ids;

public sealed record DepartmentId
{
    private DepartmentId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static DepartmentId NewDepartmentId() => new(Guid.CreateVersion7());

    public static DepartmentId Empty() => new(Guid.Empty);

    public static DepartmentId Create(Guid id) => new(id);

    public static implicit operator DepartmentId(Guid id) => new DepartmentId(id);

    public static implicit operator Guid(DepartmentId departmentId)
    {
        ArgumentNullException.ThrowIfNull(departmentId);

        return departmentId.Value;
    }

    public static DepartmentId FromGuid(Guid id) => new(id);

    public static Guid ToGuid(DepartmentId departmentId)
    {
        ArgumentNullException.ThrowIfNull(departmentId);
        return departmentId.Value;
    }
}