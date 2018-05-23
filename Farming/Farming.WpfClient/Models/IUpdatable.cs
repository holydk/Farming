using System.Threading.Tasks;

namespace Farming.WpfClient.Models
{
    public interface IUpdatable
    {
        Task UpdateAsync();
    }
}
