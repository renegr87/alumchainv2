using Alumbrado.BLL.Abstracts;
using Alumbrado.BLL.Models;
using Ipfs.Http;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Alumbrado.BLL.Services
{
    public class PublishService: IPublishService
    {
        private IConfiguration _configRoot;

        public PublishService(IConfiguration configRoot)
        {
            _configRoot = configRoot;
        }

        public Reading[] LoadReadings(string source)
        {
            if (string.IsNullOrEmpty(source)) 
            {
                throw new Exception("Fuente de datos vacía");
            }

            Reading[] readings;
            try
            {
                readings = JsonSerializer.Deserialize<Reading[]>(source);
            } catch (Exception)
            {
                throw new Exception("Error al parsear JSON invalido");
            }

            return readings;
        }

        public string Tokenize(Reading[] readings)
        {
            return "";
        }

        public bool ValidateReadings(Reading[] readings) {
            // Existen elementos
            if (readings == null || readings.Length == 0)
            {
                return false;
            }

            // Existen lecturas en 0
            if(readings.Any(r => r.Consumo == 0))
            {
                return false;
            }

            return true;
        }

        public async void PublishToIPFSAsync(string source)
        {
            var host = _configRoot["Ipfs:host"];
            var ipfsClient = new IpfsClient(host);
            await ipfsClient.FileSystem.AddFileAsync(source);
        }

    }
}