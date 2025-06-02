
namespace HospitalManagement.Infrastructure.Data;

public partial class MedicalRecord
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public DateTime? RecordDate { get; set; }

    public virtual Patient? Patient { get; set; }
}
