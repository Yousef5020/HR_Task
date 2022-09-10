namespace HR_Task.Services;

public class SalaryData : ISalaryData
{
    private readonly AppDbContext _context;

    public SalaryData(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Salary> AddSalary(Salary Salary, CancellationToken token)
    {
        var newSalary = await _context.Salaries.AddAsync(Salary, token);

        await _context.SaveChangesAsync(token);

        return newSalary.Entity;
    }

    public async Task<bool> DeleteSalary(int id, CancellationToken token)
    {
        var Salary = await GetSalary(id, token);

        if (Salary is null) return false;

        _context.Salaries.Remove(Salary);

        await _context.SaveChangesAsync(token);

        return true;
    }

    public async Task<Salary?> GetSalary(int id, CancellationToken token)
    {
        return await _context.Salaries
            .Include(e => e.JobRank)
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id, token);
    }

    public async Task<ICollection<Salary>> GetSalaries(CancellationToken token)
    {
        return await _context.Salaries
            .Include(e => e.JobRank)
            .Include(e => e.Department)
            .ToListAsync(token);
    }

    public async Task<Salary?> UpdateSalary(Salary updatedSalary, CancellationToken token)
    {
        var Salary = await GetSalary(updatedSalary.Id, token);

        if (Salary is null) return null;

        Salary.Amount = updatedSalary.Amount; 
        Salary.JobRankId = updatedSalary.JobRankId;
        Salary.DepartmentId = updatedSalary.DepartmentId;

        await _context.SaveChangesAsync(token);

        return Salary;
    }
}