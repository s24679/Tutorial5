using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorial5.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Patient_PatientIdPatient",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_PatientIdPatient",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "PatientIdPatient",
                table: "Prescription");

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[] { 3, new DateOnly(2023, 10, 10), new DateOnly(2023, 10, 25), 2, 1 });

            migrationBuilder.InsertData(
                table: "Prescription_Medicament",
                columns: new[] { "IdMedicament", "IdPrescription", "Details", "Dose" },
                values: new object[] { 2, 3, "Take twice daily", 400 });

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_IdDoctor",
                table: "Prescription",
                column: "IdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_IdPatient",
                table: "Prescription",
                column: "IdPatient");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                table: "Prescription",
                column: "IdDoctor",
                principalTable: "Doctor",
                principalColumn: "IdDoctor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Patient_IdPatient",
                table: "Prescription",
                column: "IdPatient",
                principalTable: "Patient",
                principalColumn: "IdPatient",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Patient_IdPatient",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_IdDoctor",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_IdPatient",
                table: "Prescription");

            migrationBuilder.DeleteData(
                table: "Prescription_Medicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "PatientIdPatient",
                table: "Prescription",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 1,
                column: "PatientIdPatient",
                value: null);

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 2,
                column: "PatientIdPatient",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PatientIdPatient",
                table: "Prescription",
                column: "PatientIdPatient");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Patient_PatientIdPatient",
                table: "Prescription",
                column: "PatientIdPatient",
                principalTable: "Patient",
                principalColumn: "IdPatient");
        }
    }
}
