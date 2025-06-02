using HospitalManagement.Application.DTOs;


namespace HospitalManagement.Application.IServices
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllAsync();
        Task<PatientDto> GetByIdAsync(int id);
        Task<PatientDto> CreateAsync(PatientDto dto);
        Task<PatientDto> UpdateAsync(PatientDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
