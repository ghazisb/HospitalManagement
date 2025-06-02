

namespace HospitalManagement.Application.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        //public string Notes? { get; set; } = string.Empty;
        public string? Reason { get; set; } = string.Empty;
    }
}
