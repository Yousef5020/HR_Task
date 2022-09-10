namespace HR_Task.Interfaces
{
    public interface ISalaryData
    {
        public Task<ICollection<Salary>> GetSalaries(CancellationToken token);

        public Task<Salary?> GetSalary(int id, CancellationToken token);

        public Task<Salary> AddSalary(Salary salary, CancellationToken token);

        public Task<Salary?> UpdateSalary(Salary updatedSalary, CancellationToken token);

        public Task<bool> DeleteSalary(int id, CancellationToken token);
    }
}