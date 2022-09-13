namespace HR_Task.Services;

public class AbsenceData : IAbsenceData
{
    private readonly AppDbContext _context;

    public AbsenceData(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Absence> AddAbsence(Absence absence, CancellationToken token)
    {
        var newAbsence = await _context.Absences.AddAsync(absence, token);

        await _context.SaveChangesAsync(token);

        return newAbsence.Entity;
    }

    public async Task<bool> DeleteAbsence(int id, CancellationToken token)
    {
        var Absence = await GetAbsence(id, token);

        if (Absence is null) return false;

        _context.Absences.Remove(Absence);

        await _context.SaveChangesAsync(token);

        return true;
    }

    public async Task<Absence?> GetAbsence(int id, CancellationToken token)
    {
        return await _context.Absences.Include(a => a.employee)
            .FirstOrDefaultAsync(e => e.Id == id, token);
    }

    public async Task<ICollection<Absence>> GetAbsences(CancellationToken token)
    {
        return await _context.Absences.Include(a => a.employee)
            .ToListAsync(token);
    }

    public async Task<Absence?> UpdateAbsences(Absence updatedAbsence, CancellationToken token)
    {
        var Absence = await GetAbsence(updatedAbsence.Id, token);

        if (Absence is null) return null;

        Absence.AbsenceDay = updatedAbsence.AbsenceDay;

        await _context.SaveChangesAsync(token);

        return Absence;
    }
}