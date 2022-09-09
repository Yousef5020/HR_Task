namespace HR_Task.Services
{
    public class EmployeeData : IEmployeeData
    {
        private readonly AppDbContext _context;

        public EmployeeData(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Employee> AddEmployee(Employee employee, CancellationToken token)
        {
            var newEmployee = await _context.Employees.AddAsync(employee, token);

            await _context.SaveChangesAsync(token);

            return newEmployee.Entity;
        }

        public async Task<bool> DeleteEmployee(int id, CancellationToken token)
        {
            var employee = await GetEmployee(id, token);

            if (employee is null) return false;

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync(token);

            return true;
        }

        public async Task<Employee?> GetEmployee(int id, CancellationToken token)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id, token);
        }

        public async Task<ICollection<Employee>> GetEmployees(CancellationToken token)
        {
            return await _context.Employees.ToListAsync(token);
        }

        public async Task<Employee?> UpdateEmployees(Employee updatedEmployee, CancellationToken token)
        {
            var employee = await GetEmployee(updatedEmployee.Id, token);

            if (employee is null) return null;

            employee.BirthDate = updatedEmployee.BirthDate;
            employee.PhoneNumber = updatedEmployee.PhoneNumber;
            employee.FullName = updatedEmployee.FullName;
            employee.Address = updatedEmployee.Address;
            employee.EmploymentDate = updatedEmployee.EmploymentDate;
            employee.JobRankId = updatedEmployee.JobRankId;
            employee.DepartmentId = updatedEmployee.DepartmentId;
            employee.Mail = updatedEmployee.Mail;

            await _context.SaveChangesAsync(token);

            return employee;
        }
    }
}