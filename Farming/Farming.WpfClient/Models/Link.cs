using MaterialDesignThemes.Wpf;

namespace Farming.WpfClient.Models
{
    public class Link : ImageItem, ILink
    {
        public string Adress { get; }

        public string ToolTip { get; }

        public Link(string title, string adress, PackIconKind iconKind, string toolTip = null)
            : base(title, iconKind)
        {
            Adress = adress;
            ToolTip = toolTip;
        }
    }
}
