using System;

namespace Farming.WpfClient.Models
{
    public interface INavigationItem : IImageItem
    {
        Type ViewType { get; }
    }
}
