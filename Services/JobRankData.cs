namespace HR_Task.Services;

public class JobRankData : IJobRankData
{
    private readonly AppDbContext _context;

    public JobRankData(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<JobRank> AddJobRank(JobRank JobRank, CancellationToken token)
    {
        var newJobRank = await _context.JobRanks.AddAsync(JobRank, token);

        await _context.SaveChangesAsync(token);

        return newJobRank.Entity;
    }

    public async Task<bool> DeleteJobRank(int id, CancellationToken token)
    {
        var JobRank = await GetJobRank(id, token);

        if (JobRank is null) return false;

        _context.JobRanks.Remove(JobRank);

        await _context.SaveChangesAsync(token);

        return true;
    }

    public async Task<JobRank?> GetJobRank(int id, CancellationToken token)
    {
        return await _context.JobRanks.FirstOrDefaultAsync(e => e.Id == id, token);
    }

    public async Task<ICollection<JobRank>> GetJobRanks(CancellationToken token)
    {
        return await _context.JobRanks.ToListAsync(token);
    }

    public async Task<JobRank?> UpdateJobRanks(JobRank updatedJobRank, CancellationToken token)
    {
        var JobRank = await GetJobRank(updatedJobRank.Id, token);

        if (JobRank is null) return null;

        JobRank.Name = updatedJobRank.Name;

        await _context.SaveChangesAsync(token);

        return JobRank;
    }
}