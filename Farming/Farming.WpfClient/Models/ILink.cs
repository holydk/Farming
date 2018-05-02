namespace Farming.WpfClient.Models
{
    public interface ILink : IImageItem
    {
        string Adress { get; }

        string ToolTip { get; }
    }
}
