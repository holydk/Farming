using System.Threading.Tasks;

namespace Farming.WpfClient.Models
{
    public interface IConvertFromFile
    {
        void FromFile(string fileName);

        Task FromFileAsync(string fileName);
    }
}
