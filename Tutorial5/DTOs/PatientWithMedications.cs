using Tutorial5.Models;

namespace Tutorial5.DTOs;

public class PatientWithMedicationsDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<PrescriptionWithMedicationsDto> Prescriptions { get; set; }
}

public class PrescriptionWithMedicationsDto
{
    public int IdPrescription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public List<MedicamentWithDetailsDto> Medicaments { get; set; }
    public DoctorDto Doctor { get; set; }
}

public class MedicamentWithDetailsDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
}