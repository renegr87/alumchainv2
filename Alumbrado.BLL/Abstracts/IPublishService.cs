using Alumbrado.BLL.Models;

namespace Alumbrado.BLL.Abstracts
{
    public interface IPublishService
    {
        public Reading[] LoadReadings(string source);

        public bool ValidateReadings(Reading[] readings);

        public string Tokenize(Reading[] readings);
    }
}
