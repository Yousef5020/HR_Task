namespace HR_Task.Services;

public class AttendanceRoleData : IAttendanceRoleData
{
    private readonly AppDbContext _context;

    public AttendanceRoleData(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<AttendanceRole> AddAttendanceRole(AttendanceRole AttendanceRole, CancellationToken token)
    {
        var newAttendanceRole = await _context.AttendanceRoles.AddAsync(AttendanceRole, token);

        await _context.SaveChangesAsync(token);

        return newAttendanceRole.Entity;
    }

    public async Task<bool> DeleteAttendanceRole(int id, CancellationToken token)
    {
        var AttendanceRole = await GetAttendanceRole(id, token);

        if (AttendanceRole is null) return false;

        _context.AttendanceRoles.Remove(AttendanceRole);

        await _context.SaveChangesAsync(token);

        return true;
    }

    public async Task<AttendanceRole?> GetAttendanceRole(int id, CancellationToken token)
    {
        return await _context.AttendanceRoles
            .FirstOrDefaultAsync(e => e.Id == id, token);
    }

    public async Task<ICollection<AttendanceRole>> GetAttendanceRoles(CancellationToken token)
    {
        return await _context.AttendanceRoles
            .ToListAsync(token);
    }

    public async Task<AttendanceRole?> UpdateAttendanceRoles(AttendanceRole updatedAttendanceRole, CancellationToken token)
    {
        var AttendanceRole = await GetAttendanceRole(updatedAttendanceRole.Id, token);

        if (AttendanceRole is null) return null;

        AttendanceRole.Rate = updatedAttendanceRole.Rate;

        await _context.SaveChangesAsync(token);

        return AttendanceRole;
    }
}