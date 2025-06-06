﻿using Microsoft.EntityFrameworkCore;
using Tutorial5.Models;

namespace Tutorial5.Data;

public class DatabaseContext : DbContext
{

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }


    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>() {
            new Doctor() { IdDoctor = 1, FirstName = "Thomas", LastName = "Smith", Email = "tsmith@example.com" },
            new Doctor() { IdDoctor = 2, FirstName = "Ben", LastName = "Lucas", Email = "blucas@example.com" },
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>() {
            new Patient() { IdPatient = 1, FirstName = "Elizabeth", LastName = "Swan", Birthdate = new DateOnly(2000,12,3) },
            new Patient() { IdPatient = 2, FirstName = "Adam", LastName = "Taylor", Birthdate= new DateOnly(1989, 2, 5) },
        });

        modelBuilder.Entity<Medicament>().HasData(
            new Medicament() { IdMedicament = 1, Name = "Aspirin", Description = "Pain reliever", Type = "Analgesic" },
            new Medicament() { IdMedicament = 2, Name = "Ibuprofen", Description = "Anti-inflammatory", Type = "NSAID" }
        );

        modelBuilder.Entity<Prescription>().HasData(
            new Prescription() { IdPrescription = 1, Date = new DateOnly(2023, 10, 1), DueDate = new DateOnly(2023, 10, 15), IdPatient = 1, IdDoctor = 1 },
            new Prescription() { IdPrescription = 2, Date = new DateOnly(2023, 10, 5), DueDate = new DateOnly(2023, 10, 20), IdPatient = 2, IdDoctor = 2 },
            new Prescription() { IdPrescription = 3, Date = new DateOnly(2023, 10, 10), DueDate = new DateOnly(2023, 10, 25), IdPatient = 1, IdDoctor = 2 }
        );

        modelBuilder.Entity<PrescriptionMedicament>().HasData(
            new PrescriptionMedicament() { IdPrescription = 1, IdMedicament = 1, Dose = 500, Details = "Take twice daily" },
            new PrescriptionMedicament() { IdPrescription = 1, IdMedicament = 2, Dose = 200, Details = "Take once daily" },
            new PrescriptionMedicament() { IdPrescription = 2, IdMedicament = 1, Dose = 250, Details = "Take once daily" },
            new PrescriptionMedicament() { IdPrescription = 3, IdMedicament = 2, Dose = 400, Details = "Take twice daily" }
        );
    }
}