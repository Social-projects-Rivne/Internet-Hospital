using System;
using System.Collections.Generic;
using System.Text;

namespace InternetHospital.DataAccess.AppContextConfiguration.Helpers
{
    public static class UrlHelper
    {
        public static string JsonFilesURL { get; } = @"..\InternetHospital.DataAccess\AppContextConfiguration\FileConfigurationJSON\";
        public static string StatusConfigJSON { get; } = "StatusConfigurationJSON.json";
        public static string RoleConfigJSON { get; } = "RoleConfigurationJSON.json";
        public static string UserConfigJSON { get; } = "UserConfigurationJSON.json";
        public static string SpecializationConfigJSON { get; } = "SpecializationConfigurationJSON.json";
    }
}
