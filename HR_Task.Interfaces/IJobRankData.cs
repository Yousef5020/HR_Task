namespace HR_Task.Interfaces
{
    public interface IJobRankData
    {
        public Task<ICollection<JobRank>> GetJobRanks(CancellationToken token);

        public Task<JobRank?> GetJobRank(int id, CancellationToken token);

        public Task<JobRank> AddJobRank(JobRank JobRank, CancellationToken token);

        public Task<JobRank?> UpdateJobRanks(JobRank updatedJobRank, CancellationToken token);

        public Task<bool> DeleteJobRank(int id, CancellationToken token);
    }
}