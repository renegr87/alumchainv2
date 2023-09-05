using Alumbrado.BLL.Abstracts;
using Alumbrado.BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Alumbrado
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        [STAThread]
        public static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GetConfigRoot();
            var root = GetConfigRoot();
            var jwt = root.GetConnectionString("Jwt");
            Console.WriteLine(jwt);

            //Host
            var hostBuilder = Host.CreateDefaultBuilder();


            // DI
            ConfigureServices(hostBuilder);

            //Init
            var host = hostBuilder.Build();
            ServiceProvider = host.Services;
            ApplicationConfiguration.Initialize();
            Application.Run(ServiceProvider.GetRequiredService<Main>());
        }

        private static IConfigurationRoot GetConfigRoot()
        {
            var assemblyLoc = Assembly.GetExecutingAssembly().Location;
            var directoryPath = Path.GetDirectoryName(assemblyLoc);

            var configFilePath = Path.Combine(directoryPath, "appsettings.json");

            if (File.Exists(configFilePath) == false)
            {
                throw new InvalidOperationException("Config file not found");
            }

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(configFilePath);

            var configRoot = builder.Build();

            return configRoot;
        }

        private static void ConfigureServices(IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IPublishService, PublishService>();
                    services.AddTransient<Main>();
                });
        }
    }
}