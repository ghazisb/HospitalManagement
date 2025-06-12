


namespace HospitalManagement.Application.DTOs
{
    public class MedicalRecordDto
    {
        public int Id { get; set; }

        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }

        public string? Diagnosis { get; set; }

        public string? Treatment { get; set; }

        public DateTime? RecordDate { get; set; }


        // Include Patient Info
        public string? PatientName { get; set; }

        public string? DoctorName { get; set; }
        

        //public virtual PatientDto? Patient { get; set; }
    }
}
