
using HospitalManagement.Application.DTOs;

namespace HospitalManagement.Application.IServices
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAsync();
        Task<AppointmentDto> GetByIdAsync(int id);
        Task<AppointmentDto> CreateAsync(AppointmentDto dto);
        Task<AppointmentDto> UpdateAsync(AppointmentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
