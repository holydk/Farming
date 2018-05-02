using Farming.WpfClient.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace Farming.WpfClient.ViewModels
{
    public interface IInformationPageViewModel : IPageViewModel
    {
        IEnumerable<ILink> InformationLinks { get; }

        IEnumerable<IInformationItem> InformationItems { get; }

        ICommand OpenLinkCommand { get; }
    }
}
