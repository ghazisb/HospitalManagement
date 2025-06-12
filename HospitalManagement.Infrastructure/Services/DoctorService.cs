
using HospitalManagement.Application.DTOs;
using HospitalManagement.Application.IServices;
using HospitalManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Numerics;


namespace HospitalManagement.Domain.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly HospitalDbContext _context;

        public DoctorService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorDto>> GetAllAsync() => await _context.Doctors.Select(d => new DoctorDto
        {
            Id = d.Id,
            FirstName = d.FirstName,
            LastName = d.LastName,
            Specialty = d.Specialty,
            Phone = d.Phone
        }).ToListAsync();

        public async Task<DoctorDto> GetByIdAsync(int id) => await _context.Doctors.Where(d => d.Id == id).Select(d => new DoctorDto
        {
            Id = d.Id,
            FirstName = d.FirstName,
            LastName = d.LastName,
            Specialty = d.Specialty,
            Phone = d.Phone
        }).FirstOrDefaultAsync();

        public async Task<DoctorDto> CreateAsync(DoctorDto dto)
        {
            try
            {
                var doctor = new Doctor
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Specialty = dto.Specialty,
                    Phone = dto.Phone
                };
                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
                dto.Id = doctor.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
            return dto;
        }

        public async Task<DoctorDto> UpdateAsync(DoctorDto dto)
        {
            try
            {
                var doctor = await _context.Doctors.FindAsync(dto.Id);
                if (doctor == null) return null!;

                doctor.FirstName = dto.FirstName;
                doctor.LastName = dto.LastName;
                doctor.Specialty = dto.Specialty;
                doctor.Phone = dto.Phone;

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
                var doctor = await _context.Doctors.FindAsync(id);
                if (doctor == null) return false;
                _context.Doctors.Remove(doctor);
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
