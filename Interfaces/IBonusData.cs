namespace HR_Task.Interfaces
{
    public interface IBonusData
    {
        public Task<ICollection<Bonus>> GetBonuses(CancellationToken token);

        public Task<Bonus?> GetBonus(int id, CancellationToken token);

        public Task<Bonus> AddBonus(Bonus Bonus, CancellationToken token);

        public Task<Bonus?> UpdateBonus(Bonus updatedBonus, CancellationToken token);

        public Task<bool> DeleteBonus(int id, CancellationToken token);

        public Task<ICollection<BonusType>> GetBonusTypes(CancellationToken token);
    }
}