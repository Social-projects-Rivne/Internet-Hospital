using InternetHospital.DataAccess.AppContextConfiguration.Helpers;
using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            string jsonString = File.ReadAllText(UrlHelper.JsonFilesURL + UrlHelper.SpecializationConfigJSON);
            var initialSpecializations = new List<Specialization>();
            var jsonSpecializations = JArray.Parse(jsonString);

            foreach (dynamic item in jsonSpecializations)
            {
                initialSpecializations.Add(new Specialization { Id = item.Id, Name = item.Name, Description = item.Description });
            }

            builder.HasData(initialSpecializations.ToArray());
        }
    }
}
