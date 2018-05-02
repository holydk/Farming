using MaterialDesignThemes.Wpf;

namespace Farming.WpfClient.Models
{
    public interface IImageItem : ITabItem
    {
        PackIconKind IconKind { get; }
    }
}
