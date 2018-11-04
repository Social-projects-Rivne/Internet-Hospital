using InternetHospital.DataAccess.AppContextConfiguration.Helpers;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace InternetHospital.DataAccess.AppContextConfiguration
{
    public class RoleConfiguration
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            try
            {
                string jsonString = File.ReadAllText(UrlHelper.JsonFilesURL + UrlHelper.RoleConfigJSON);
                string role = null;
                var jsonRoles = JArray.Parse(jsonString);

                foreach (dynamic item in jsonRoles)
                {
                    //It's necessary because i had an exception in runtime execution
                    role = item.Name;
                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole<int>(role));
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Check if file exist and path is correct: {ex.Message}");
                throw;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Something wrong with file or path, its better to check them: {ex.Message}");
                throw;
            }
        }
    }
}
