using HospitalManagement.Application.DTOs;
using HospitalManagement.Application.IServices;
using HospitalManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;
using System.Reflection;

namespace HospitalManagement.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly HospitalDbContext _context;

        public PatientService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PatientDto>> GetAllAsync() => await _context.Patients.Select(p => new PatientDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Gender = p.Gender,
            DateOfBirth = p.DateOfBirth,
            Phone = p.Phone,
            Address = p.Address,
        }).ToListAsync();

        public async Task<PatientDto> GetByIdAsync(int id) => await _context.Patients.Where(p => p.Id == id).Select(p => new PatientDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Gender = p.Gender,
            DateOfBirth = p.DateOfBirth,
            Phone = p.Phone,
            Address = p.Address,
        }).FirstOrDefaultAsync();

        public async Task<PatientDto> CreateAsync(PatientDto dto)
        {
            var patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                Phone = dto.Phone,
                Address = dto.Address,
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            dto.Id = patient.Id;
            return dto;
        }

        public async Task<PatientDto> UpdateAsync(PatientDto dto)
        {
            var patient = await _context.Patients.FindAsync(dto.Id);
            if (patient == null) return null!;

            patient.FirstName = dto.FirstName;
            patient.LastName = dto.LastName;
            patient.Gender = dto.Gender;
            patient.DateOfBirth = dto.DateOfBirth;
            patient.Phone = dto.Phone;
            patient.Address = dto.Address;
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
