using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.DTOs;

namespace Tutorial5.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    


    public async Task<List<PatientWithMedicationsDto>> GetPatients()
    {
var patients = await _context.Patients
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
            .Select(e => new PatientWithMedicationsDto
            {
                IdPatient = e.IdPatient,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Prescriptions = e.Prescriptions.OrderBy(p => p.DueDate).Select(p => new PrescriptionWithMedicationsDto
                {
                    IdPrescription = p.IdPrescription,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    Medicaments = p.PrescriptionMedicaments.Select(m => new MedicamentWithDetailsDto
                    {
                        IdMedicament = m.Medicament.IdMedicament,
                        Name = m.Medicament.Name,
                        Description = m.Medicament.Description,
                        Type = m.Medicament.Type,
                        Dose = m.Dose,
                        Details = m.Details
                    }).ToList(),
                    Doctor = new DoctorDto
                    {
                        IdDoctor = p.Doctor.IdDoctor,
                        FirstName = p.Doctor.FirstName,
                        LastName = p.Doctor.LastName,
                        Email = p.Doctor.Email
                    }
                }).ToList()
            }).ToListAsync();
        return patients;
    }

    public async Task<PatientWithMedicationsDto?> GetPatient(int id)
    {
        var patients = await _context.Patients
                    .Include(p => p.Prescriptions)
                        .ThenInclude(pr => pr.PrescriptionMedicaments)
                            .ThenInclude(pm => pm.Medicament)
                    .Where(p => p.IdPatient == id)
                    .Select(e => new PatientWithMedicationsDto
                    {
                        IdPatient = e.IdPatient,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Prescriptions = e.Prescriptions.OrderBy(p => p.DueDate).Select(p => new PrescriptionWithMedicationsDto
                        {
                            IdPrescription = p.IdPrescription,
                            Date = p.Date,
                            DueDate = p.DueDate,
                            Medicaments = p.PrescriptionMedicaments.Select(m => new MedicamentWithDetailsDto
                            {
                                IdMedicament = m.Medicament.IdMedicament,
                                Name = m.Medicament.Name,
                                Description = m.Medicament.Description,
                                Type = m.Medicament.Type,
                                Dose = m.Dose,
                                Details = m.Details
                            }).ToList(),
                            Doctor = new DoctorDto
                            {
                                IdDoctor = p.Doctor.IdDoctor,
                                FirstName = p.Doctor.FirstName,
                                LastName = p.Doctor.LastName,
                                Email = p.Doctor.Email
                            }
                        }).ToList()
                    }).FirstOrDefaultAsync();
        return patients;
    }

    public async Task<int> AddPrescription(AddPrescriptionDto request)
    {
        if (request.Medicaments.Count > 10)
            throw new ArgumentException("Prescription cannot contain more than 10 medicaments");

        if (request.DueDate < request.Date)
            throw new ArgumentException("DueDate < Date");

        var medicamentIds = request.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicaments = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();

        if (existingMedicaments.Count != medicamentIds.Count)
            throw new ArgumentException("Medicaments do not exist");

        var patient = await _context.Patients
            .FirstOrDefaultAsync(p =>
                p.FirstName == request.Patient.FirstName &&
                p.LastName == request.Patient.LastName &&
                p.Birthdate == request.Patient.Birthdate);

        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d =>
                d.FirstName == request.Doctor.FirstName &&
                d.LastName == request.Doctor.LastName &&
                d.Email == request.Doctor.Email);

        if (doctor == null) {
            throw new ArgumentException("Doctor does not exist");
        }

            if (patient == null)
        {
            patient = new Models.Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        var prescription = new Models.Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor,
            PrescriptionMedicaments = request.Medicaments.Select(m => new Models.PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return prescription.IdPrescription;
    }

}