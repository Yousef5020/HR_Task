namespace HR_Task.Interfaces
{
    public interface IDepartmentData
    {
        public Task<ICollection<Department>> GetDepartments(CancellationToken token);

        public Task<Department?> GetDepartment(int id, CancellationToken token);

        public Task<Department> AddDepartment(Department department, CancellationToken token);

        public Task<Department?> UpdateDepartments(Department updatedDepartment, CancellationToken token);

        public Task<bool> DeleteDepartment(int id, CancellationToken token);
    }
}