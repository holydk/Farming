using System.Windows.Input;

namespace Farming.WpfClient.Models
{
    public interface ICommandItem : IImageItem
    {
        ICommand Command { get; }
    }
}
