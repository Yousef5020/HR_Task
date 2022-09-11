namespace HR_Task.Interfaces
{
    public interface IAttendanceRoleData
    {
        public Task<ICollection<AttendanceRole>> GetAttendanceRoles(CancellationToken token);

        public Task<AttendanceRole?> GetAttendanceRole(int id, CancellationToken token);

        public Task<AttendanceRole> AddAttendanceRole(AttendanceRole attendanceRole, CancellationToken token);

        public Task<AttendanceRole?> UpdateAttendanceRoles(AttendanceRole updatedAttendanceRole, CancellationToken token);

        public Task<bool> DeleteAttendanceRole(int id, CancellationToken token);
    }
}