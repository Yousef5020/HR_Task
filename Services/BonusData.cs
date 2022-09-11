using HR_Task.Models;

namespace HR_Task.Services;

public class BonusData : IBonusData
{
    private readonly AppDbContext _context;

    public BonusData(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Bonus> AddBonus(Bonus Bonus, CancellationToken token)
    {
        if (Bonus.TypeId == 1) Bonus.Role = Bonus.RoleDepartment;

        var newBonus = await _context.Bonus.AddAsync(Bonus, token);

        await _context.SaveChangesAsync(token);

        return newBonus.Entity;
    }

    public async Task<bool> DeleteBonus(int id, CancellationToken token)
    {
        var Bonus = await GetBonus(id, token);

        if (Bonus is null) return false;

        _context.Bonus.Remove(Bonus);

        await _context.SaveChangesAsync(token);

        return true;
    }

    public async Task<Bonus?> GetBonus(int id, CancellationToken token)
    {
        return await _context.Bonus
            .Include(b => b.BonusType)
            .FirstOrDefaultAsync(e => e.Id == id, token);
    }

    public async Task<ICollection<Bonus>> GetBonuses(CancellationToken token)
    {
        var departmentBonus = await _context.Bonus
            .Include(b => b.BonusType)
            .Join(_context.Departments,
            b => b.Role,
            d => d.Id,
            (b, d) => new Bonus
            {
                Id = b.Id,
                Rate = b.Rate,
                Role = b.Role,
                TypeId = b.TypeId,
                RoleDescreption = d.Name + " - " + b.BonusType.Name,
                BonusType = b.BonusType
            })
            .Where(b => b.TypeId == 1)
            .ToListAsync(token);

        var yearlyBonus = await _context.Bonus
            .Include(b => b.BonusType)
            .Where(b => b.TypeId == 2)
            .Select(b => new Bonus
            {
                Id = b.Id,
                Rate = b.Rate,
                Role = b.Role,
                TypeId = b.TypeId,
                RoleDescreption = b.Role + " - " + b.BonusType.Name,
                BonusType = b.BonusType
            }).ToListAsync(token);

        departmentBonus.AddRange(yearlyBonus);

        return departmentBonus;
    }

    public async Task<ICollection<BonusType>> GetBonusTypes(CancellationToken token)
    {
        return await _context.BonusTypes.ToListAsync(token);
    }

    public async Task<Bonus?> UpdateBonus(Bonus updatedBonus, CancellationToken token)
    {
        var Bonus = await GetBonus(updatedBonus.Id, token);

        if (Bonus is null) return null;

        Bonus.Rate = updatedBonus.Rate;


        await _context.SaveChangesAsync(token);

        return Bonus;
    }
}