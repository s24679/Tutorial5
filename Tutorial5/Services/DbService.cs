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
    
    public async Task<List<BookWithAuthorsDto>> GetBooks()
    {
        var books = await _context.Books.Select(e =>
        new BookWithAuthorsDto {
            Name = e.Name,
            Price = e.Price,
            Authors = e.BookAuthors.Select(a =>
            new AuthorDto {
                FirstName = a.Author.FirstName,
                LastName = a.Author.LastName
            }).ToList()
        }).ToListAsync();
        return books;
    }

    public async Task<List<PatientWithMedicationsDto>> GetPatients()
    {
var patients = await _context.Patients
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
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
                    Doctor = p.Doctor
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
                            Doctor = p.Doctor
                        }).ToList()
                    }).FirstOrDefaultAsync();
        return patients;
    }

}