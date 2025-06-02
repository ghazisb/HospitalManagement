
using HospitalManagement.Application.DTOs;

namespace HospitalManagement.Application.IServices
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllAsync();
        Task<DoctorDto> GetByIdAsync(int id);
        Task<DoctorDto> CreateAsync(DoctorDto dto);
        Task<DoctorDto> UpdateAsync(DoctorDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
