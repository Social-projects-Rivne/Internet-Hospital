using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            #region InitialData
            int id = 1;
            var initialSpecializations = new List<Specialization>
            {
                new Specialization
                {
                    Id = id++,
                    Name = "Allergist",
                    Description = "Conducts the diagnosis and treatment of allergic conditions."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Anesthesiologist",
                    Description = "Treats chronic pain syndromes; administers anesthesia and monitors the patient during surgery."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Pathologist",
                    Description = "Diagnoses and treats the study of the changes in body tissues and organs which cause or are caused by disease."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Cardiologist",
                    Description = "Treats heart disease."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Endocrinologist",
                    Description = "Dealing with the endocrine system, its diseases, and its specific secretions known as hormones."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Gastroenterologist",
                    Description = "Treats stomach disorders."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Dermatologist",
                    Description = "Treats skin diseases, including some skin cancers."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Hematologist/Oncologist",
                    Description = "Treats diseases of the blood and blood-forming tissues (oncology including cancer and other tumors)."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Internal Medicine Physician",
                    Description = "Treats diseases and disorders of internal structures of the body."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Nephrologist",
                    Description = "Treats kidney diseases."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Neurologist",
                    Description = "Treats diseases and disorders of the nervous system."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Neurosurgeon",
                    Description = "Conducts surgery of the nervous system."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Obstetrician",
                    Description = "Treats women during pregnancy and childbirth."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Gynecologist",
                    Description = "Treats diseases of the female reproductive system and genital tract."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Occupational Medicine Physician",
                    Description = "Diagnoses and treats work-related disease or injury."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Ophthalmologist",
                    Description = "Treats eye defects, injuries, and diseases."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Oral and Maxillofacial Surgeon",
                    Description = "Surgically treats diseases, injuries, and defects of the hard and soft tissues of the face, mouth, and jaws."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Orthopaedic Surgeon",
                    Description = "Preserves and restores the function of the musculoskeletal system."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Otolaryngologist",
                    Description = "Treats diseases of the ear, nose, and throat,and some diseases of the head and neck, including facial plastic surgery."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Pediatrician",
                    Description = "Treats infants, toddlers, children and teenagers."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Plastic Surgeon",
                    Description = "Restores, reconstructs, corrects or improves in the shape and appearance of damaged body structures, especially the face."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Podiatrist",
                    Description = "Provides medical and surgical treatment of the foot."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Psychiatrist",
                    Description = "Treats patients with mental and emotional disorders."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Pulmonary Medicine Physician",
                    Description = "Diagnoses and treats lung disorders."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Rheumatologist",
                    Description = "Treats rheumatic diseases, or conditions characterized by inflammation, soreness and stiffness of muscles, and pain in joints and associated structures."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Urologist",
                    Description = "Diagnoses and treats the male and female urinary tract and the male reproductive system."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Ophthalmologist",
                    Description = "Branch of medicine and surgery (both methods are used) that deals with the anatomy, physiology and diseases of the eyeball and orbit."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Dentist",
                    Description = "Is a surgeon who specializes in dentistry, the diagnosis, prevention, and treatment of diseases and conditions of the oral cavity."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Neonatologist",
                    Description = "Is a subspecialty of pediatrics that consists of the medical care of newborn infants, especially the ill or premature newborn."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Pulmonologist",
                    Description = "Is a medical speciality that deals with diseases involving the respiratory tract."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Radiologist",
                    Description = "Is the medical specialty that uses medical imaging to diagnose and treat diseases within the body."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "General Surgeon",
                    Description = "Is a surgical specialty that focuses on abdominal contents including esophagus, stomach, small bowel, colon, liver, pancreas, gallbladder, appendix and bile ducts, and often the thyroid gland."
                },
                new Specialization
                {
                    Id = id++,
                    Name = "Vascular Surgeon",
                    Description = "Is a surgical subspecialty in which diseases of the vascular system, or arteries, veins and lymphatic circulation, are managed by medical therapy, minimally-invasive catheter procedures, and surgical reconstruction."
                }
            };
            #endregion

            builder.HasData(initialSpecializations.ToArray());
        }
    }
}