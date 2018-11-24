using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetHospital.WebApi.Migrations
{
    public partial class Changeillnesshistoryentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Treatment",
                table: "IllnessHistories",
                newName: "TreatmentPlan");

            migrationBuilder.RenameColumn(
                name: "Symptoms",
                table: "IllnessHistories",
                newName: "SurveyPlan");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "IllnessHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complaints",
                table: "IllnessHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiseaseAnamnesis",
                table: "IllnessHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LifeAnamnesis",
                table: "IllnessHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalStatus",
                table: "IllnessHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObjectiveStatus",
                table: "IllnessHistories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IllnessHistories_AppointmentId",
                table: "IllnessHistories",
                column: "AppointmentId",
                unique: true,
                filter: "[AppointmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_IllnessHistories_Appointments_AppointmentId",
                table: "IllnessHistories",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IllnessHistories_Appointments_AppointmentId",
                table: "IllnessHistories");

            migrationBuilder.DropIndex(
                name: "IX_IllnessHistories_AppointmentId",
                table: "IllnessHistories");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "IllnessHistories");

            migrationBuilder.DropColumn(
                name: "Complaints",
                table: "IllnessHistories");

            migrationBuilder.DropColumn(
                name: "DiseaseAnamnesis",
                table: "IllnessHistories");

            migrationBuilder.DropColumn(
                name: "LifeAnamnesis",
                table: "IllnessHistories");

            migrationBuilder.DropColumn(
                name: "LocalStatus",
                table: "IllnessHistories");

            migrationBuilder.DropColumn(
                name: "ObjectiveStatus",
                table: "IllnessHistories");

            migrationBuilder.RenameColumn(
                name: "TreatmentPlan",
                table: "IllnessHistories",
                newName: "Treatment");

            migrationBuilder.RenameColumn(
                name: "SurveyPlan",
                table: "IllnessHistories",
                newName: "Symptoms");
        }
    }
}
