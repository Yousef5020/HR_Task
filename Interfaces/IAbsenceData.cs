namespace HR_Task.Interfaces
{
    public interface IAbsenceData
    {
        public Task<ICollection<Absence>> GetAbsences(CancellationToken token);

        public Task<Absence?> GetAbsence(int id, CancellationToken token);

        public Task<Absence> AddAbsence(Absence absence, CancellationToken token);

        public Task<Absence?> UpdateAbsences(Absence updatedAbsence, CancellationToken token);

        public Task<bool> DeleteAbsence(int id, CancellationToken token);
    }
}