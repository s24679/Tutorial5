using Tutorial5.DTOs;

namespace Tutorial5.Services;

public interface IDbService
{
    Task<List<BookWithAuthorsDto>> GetBooks();
    Task<List<PatientWithMedicationsDto>> GetPatients();
    Task<PatientWithMedicationsDto> GetPatient(int id);

}