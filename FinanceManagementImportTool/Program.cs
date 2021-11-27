using Microsoft.Extensions.Configuration;

namespace FinanceManagementImportTool
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            ApplicationConfiguration.Initialize();
            Application.Run(new Import(configuration["APIBaseUrl"]));

        }

    }
}