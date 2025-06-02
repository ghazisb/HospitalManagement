
using HospitalManagement.Application.DTOs;
using HospitalManagement.Application.IServices;
using HospitalManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace HospitalManagement.Infrastructure.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HospitalDbContext _context;

        public AppointmentService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAsync() => await _context.Appointments.Select(a => new AppointmentDto
        {
            Id = a.Id,
            PatientId = a.PatientId,
            DoctorId = a.DoctorId,
            AppointmentDate = a.AppointmentDate,
            Reason = a.Reason
        }).ToListAsync();

        public async Task<AppointmentDto> GetByIdAsync(int id) => await _context.Appointments.Where(a => a.Id == id).Select(a => new AppointmentDto
        {
            Id = a.Id,
            PatientId = a.PatientId,
            DoctorId = a.DoctorId,
            AppointmentDate = a.AppointmentDate,
            Reason = a.Reason
        }).FirstOrDefaultAsync();

        public async Task<AppointmentDto> CreateAsync(AppointmentDto dto)
        {
            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate,
                Reason = dto.Reason
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            dto.Id = appointment.Id;
            return dto;
        }

        public async Task<AppointmentDto> UpdateAsync(AppointmentDto dto)
        {
            var appointment = await _context.Appointments.FindAsync(dto.Id);
            if (appointment == null) return null!;
            appointment.PatientId = dto.PatientId;
            appointment.DoctorId = dto.DoctorId;
            appointment.AppointmentDate = dto.AppointmentDate;
            appointment.Reason = dto.Reason;
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return false;
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
