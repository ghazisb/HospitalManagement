using HospitalManagement.Application.DTOs;


namespace HospitalManagement.Application.IServices
{
    public interface IMedicalRecordService
    {
        Task<IEnumerable<MedicalRecordDto>> GetAllAsync();
        Task<MedicalRecordDto> GetByIdAsync(int id);
        Task<MedicalRecordDto> CreateAsync(MedicalRecordDto dto);
        Task<MedicalRecordDto> UpdateAsync(MedicalRecordDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
