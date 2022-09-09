namespace HR_Task.Interfaces
{
    public interface IEmployeeData
    {
        public Task<ICollection<Employee>> GetEmployees(CancellationToken token);

        public Task<Employee?> GetEmployee(int id, CancellationToken token);

        public Task<Employee> AddEmployee(Employee employee, CancellationToken token);

        public Task<Employee?> UpdateEmployees(Employee updatedEmployee, CancellationToken token);

        public Task<bool> DeleteEmployee(int id, CancellationToken token);
    }
}