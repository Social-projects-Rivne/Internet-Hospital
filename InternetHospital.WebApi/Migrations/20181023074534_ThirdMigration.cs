using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetHospital.WebApi.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Conducts the diagnosis and treatment of allergic conditions.", "Allergist" },
                    { 31, "Is the medical specialty that uses medical imaging to diagnose and treat diseases within the body.", "Radiologist" },
                    { 30, "Is a medical speciality that deals with diseases involving the respiratory tract.", "Pulmonologist" },
                    { 29, "Is a subspecialty of pediatrics that consists of the medical care of newborn infants, especially the ill or premature newborn.", "Neonatologist" },
                    { 28, "Is a surgeon who specializes in dentistry, the diagnosis, prevention, and treatment of diseases and conditions of the oral cavity.", "Dentist" },
                    { 27, "Branch of medicine and surgery (both methods are used) that deals with the anatomy, physiology and diseases of the eyeball and orbit.", "Ophthalmologist" },
                    { 26, "Diagnoses and treats the male and female urinary tract and the male reproductive system.", "Urologist" },
                    { 25, "Treats rheumatic diseases, or conditions characterized by inflammation, soreness and stiffness of muscles, and pain in joints and associated structures.", "Rheumatologist" },
                    { 24, "Diagnoses and treats lung disorders.", "Pulmonary Medicine Physician" },
                    { 23, "Treats patients with mental and emotional disorders.", "Psychiatrist" },
                    { 22, "Provides medical and surgical treatment of the foot.", "Podiatrist" },
                    { 21, "Restores, reconstructs, corrects or improves in the shape and appearance of damaged body structures, especially the face.", "Plastic Surgeon" },
                    { 20, "Treats infants, toddlers, children and teenagers.", "Pediatrician" },
                    { 19, "Treats diseases of the ear, nose, and throat,and some diseases of the head and neck, including facial plastic surgery.", "Otolaryngologist" },
                    { 18, "Preserves and restores the function of the musculoskeletal system.", "Orthopaedic Surgeon" },
                    { 32, "Is a surgical specialty that focuses on abdominal contents including esophagus, stomach, small bowel, colon, liver, pancreas, gallbladder, appendix and bile ducts, and often the thyroid gland.", "General Surgeon" },
                    { 17, "Surgically treats diseases, injuries, and defects of the hard and soft tissues of the face, mouth, and jaws.", "Oral and Maxillofacial Surgeon" },
                    { 15, "Diagnoses and treats work-related disease or injury.", "Occupational Medicine Physician" },
                    { 14, "Treats diseases of the female reproductive system and genital tract.", "Gynecologist" },
                    { 13, "Treats women during pregnancy and childbirth.", "Obstetrician" },
                    { 12, "Conducts surgery of the nervous system.", "Neurosurgeon" },
                    { 11, "Treats diseases and disorders of the nervous system.", "Neurologist" },
                    { 10, "Treats kidney diseases.", "Nephrologist" },
                    { 9, "Treats diseases and disorders of internal structures of the body.", "Internal Medicine Physician" },
                    { 8, "Treats diseases of the blood and blood-forming tissues (oncology including cancer and other tumors).", "Hematologist/Oncologist" },
                    { 7, "Treats skin diseases, including some skin cancers.", "Dermatologist" },
                    { 6, "Treats stomach disorders.", "Gastroenterologist" },
                    { 5, "Dealing with the endocrine system, its diseases, and its specific secretions known as hormones.", "Endocrinologist" },
                    { 4, "Treats heart disease.", "Cardiologist" },
                    { 3, "Diagnoses and treats the study of the changes in body tissues and organs which cause or are caused by disease.", "Pathologist" },
                    { 2, "Treats chronic pain syndromes; administers anesthesia and monitors the patient during surgery.", "Anesthesiologist" },
                    { 16, "Treats eye defects, injuries, and diseases.", "Ophthalmologist" },
                    { 33, "Is a surgical subspecialty in which diseases of the vascular system, or arteries, veins and lymphatic circulation, are managed by medical therapy, minimally-invasive catheter procedures, and surgical reconstruction.", "Vascular Surgeon" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 33);
        }
    }
}
