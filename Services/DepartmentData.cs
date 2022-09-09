namespace HR_Task.Services;

public class DepartmentData : IDepartmentData
{
    private readonly AppDbContext _context;

    public DepartmentData(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Department> AddDepartment(Department Department, CancellationToken token)
    {
        var newDepartment = await _context.Departments.AddAsync(Department, token);

        await _context.SaveChangesAsync(token);

        return newDepartment.Entity;
    }

    public async Task<bool> DeleteDepartment(int id, CancellationToken token)
    {
        var Department = await GetDepartment(id, token);

        if (Department is null) return false;

        _context.Departments.Remove(Department);

        await _context.SaveChangesAsync(token);

        return true;
    }

    public async Task<Department?> GetDepartment(int id, CancellationToken token)
    {
        return await _context.Departments.FirstOrDefaultAsync(e => e.Id == id, token);
    }

    public async Task<ICollection<Department>> GetDepartments(CancellationToken token)
    {
        return await _context.Departments.ToListAsync(token);
    }

    public async Task<Department?> UpdateDepartments(Department updatedDepartment, CancellationToken token)
    {
        var Department = await GetDepartment(updatedDepartment.Id, token);

        if (Department is null) return null;

        Department.Name = updatedDepartment.Name;

        await _context.SaveChangesAsync(token);

        return Department;
    }
}