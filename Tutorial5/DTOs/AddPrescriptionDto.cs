using Tutorial5.Models;

namespace Tutorial5.DTOs;

public class AddPrescriptionDto
{
    public PatientDto Patient { get; set; }
    public List<PrescriptionMedicamentDto> Medicaments { get; set; }
    public DoctorDto Doctor { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
}

public class PatientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
}

public class PrescriptionDto
{
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
}

public class PrescriptionMedicamentDto
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
}
