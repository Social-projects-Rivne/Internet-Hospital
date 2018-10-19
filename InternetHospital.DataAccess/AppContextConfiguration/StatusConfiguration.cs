using InternetHospital.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            const string PATH = @"..\InternetHospital.DataAccess\AppContextConfiguration\StatusConfigurationJSON.json";

            string jsonString = File.ReadAllText(PATH);

            var jsonStatuses = JArray.Parse(jsonString);

            var initialStatuses = new List<Status>();

            foreach (dynamic item in jsonStatuses)
            {
                initialStatuses.Add(new Status { Id = item.Id, Name = item.Name, Description = item.Description });
            }

            builder.HasData(initialStatuses.ToArray());
        }
    }
}
