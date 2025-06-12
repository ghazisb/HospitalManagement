using HospitalManagement.Application.DTOs;
using HospitalManagement.Application.IServices;
using HospitalManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;
using System.Reflection;

namespace HospitalManagement.Infrastructure.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly HospitalDbContext _context;

        public MedicalRecordService(HospitalDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<MedicalRecordDto>> GetAllAsync() => await _context.MedicalRecords.Select(p => new MedicalRecordDto
        //{

        //    Id = p.Id,
        //    PatientId = p.PatientId,
        //    Diagnosis = p.Diagnosis,
        //    Treatment = p.Treatment,
        //    RecordDate = p.RecordDate
        //}).ToListAsync();


        public async Task<IEnumerable<MedicalRecordDto>> GetAllAsync()
        {
            var results =
             await _context.MedicalRecords
                .Include(r => r.Patient) // Ensure patient data is fetched
                .Select(r => new MedicalRecordDto
                {
                    Id = r.Id,
                    PatientId = r.PatientId,
                    DoctorId = r.DoctorId,
                    Diagnosis = r.Diagnosis,
                    Treatment = r.Treatment,
                    RecordDate = r.RecordDate,
                    PatientName = r.Patient != null
                                  ? r.Patient.FirstName + " " + r.Patient.LastName
                                  : null,
                    DoctorName = r.Doctor != null
                                  ? r.Doctor.FirstName + " " + r.Doctor.LastName
                                  : null
                }).ToListAsync();
            return results;
        }

        //public async Task<MedicalRecordDto> GetByIdAsync(int id) => await _context.MedicalRecords.Where(p => p.Id == id).Select(p => new MedicalRecordDto
        //{
        //    Id = p.Id,
        //    PatientId = p.PatientId,
        //    Diagnosis = p.Diagnosis,
        //    Treatment = p.Treatment,
        //    RecordDate = p.RecordDate,
        //}).FirstOrDefaultAsync();


        public async Task<MedicalRecordDto> GetByIdAsync(int id)
        {
            return await _context.MedicalRecords
                .Include(p => p.Patient) // Ensure related patient is loaded
                .Where(p => p.Id == id)
                .Select(p => new MedicalRecordDto
                {
                    Id = p.Id,
                    PatientId = p.PatientId,
                    DoctorId = p.DoctorId,
                    Diagnosis = p.Diagnosis,
                    Treatment = p.Treatment,
                    RecordDate = p.RecordDate,
                    PatientName = p.Patient != null
                                  ? p.Patient.FirstName + " " + p.Patient.LastName
                                  : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<MedicalRecordDto> CreateAsync(MedicalRecordDto dto)
        {
            try
            {
                var medicalRecord = new MedicalRecord
                {
                    Id = dto.Id,
                    PatientId = dto.PatientId,
                    DoctorId = dto.DoctorId,
                    Diagnosis = dto.Diagnosis,
                    Treatment = dto.Treatment,
                    RecordDate = dto.RecordDate
                };
                _context.MedicalRecords.Add(medicalRecord);
                await _context.SaveChangesAsync();
                dto.Id = medicalRecord.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
            return dto;
        }

        public async Task<MedicalRecordDto> UpdateAsync(MedicalRecordDto dto)
        {
            try
            {
                var medicalRecord = await _context.MedicalRecords.FindAsync(dto.Id);
                if (medicalRecord == null) return null!;

                medicalRecord.Id = dto.Id;
                medicalRecord.PatientId = dto.PatientId;
                medicalRecord.DoctorId = dto.DoctorId;
                medicalRecord.Diagnosis = dto.Diagnosis;
                medicalRecord.Treatment = dto.Treatment;
                medicalRecord.RecordDate = dto.RecordDate;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var medicalRecord = await _context.MedicalRecords.FindAsync(id);
                if (medicalRecord == null) return false;
                _context.MedicalRecords.Remove(medicalRecord);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }
    }
}
