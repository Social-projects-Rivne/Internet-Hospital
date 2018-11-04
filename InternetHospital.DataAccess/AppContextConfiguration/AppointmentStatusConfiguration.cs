using InternetHospital.DataAccess.AppContextConfiguration.Helpers;
using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    class AppointmenStatusConfiguration : IEntityTypeConfiguration<AppointmentStatus>
    {
        public void Configure(EntityTypeBuilder<AppointmentStatus> builder)
        {
            string jsonString = File.ReadAllText(UrlHelper.JsonFilesURL + UrlHelper.AppointmentStatusConfigJSON);
            var initialStatuses = new List<AppointmentStatus>();
            var jsonStatuses = JArray.Parse(jsonString);

            foreach (dynamic item in jsonStatuses)
            {
                initialStatuses.Add(new AppointmentStatus { Id = item.Id, Name = item.Name, Description = item.Description });
            }

            builder.HasData(initialStatuses.ToArray());
        }
    }
}
