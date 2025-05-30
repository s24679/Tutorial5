﻿using Tutorial5.DTOs;

namespace Tutorial5.Services;

public interface IDbService
{
    Task<List<PatientWithMedicationsDto>> GetPatients();
    Task<PatientWithMedicationsDto> GetPatient(int id);
    Task<int> AddPrescription(AddPrescriptionDto request);

}