namespace HR_Task.Interfaces
{
    public interface IEmployeeData
    {
        public Task<ICollection<Employee>> GetEmployees();

        public Task<Employee> GetEmployee(int id);

        public Task<Employee> AddEmployee(Employee employee);

        public Task<Employee> UpdateEmployees(Employee updatedEmployee);

        public Task<bool> DeleteEmployee(int id);
    }
}