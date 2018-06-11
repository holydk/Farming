using System.Collections.Generic;
using System.Windows.Input;
using Farming.WpfClient.Commands;
using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;

namespace Farming.WpfClient.ViewModels
{
    public class InformationPageViewModel : PageViewModel, IInformationPageViewModel
    {
        protected IEnumerable<IInformationItem> _informationItems;
        public IEnumerable<IInformationItem> InformationItems => _informationItems;

        protected IEnumerable<ILink> _informationLinks;
        public IEnumerable<ILink> InformationLinks => _informationLinks;

        public ICommand OpenLinkCommand { get; }

        public InformationPageViewModel(string title, ISnackbarMessageQueue snackbarMessageQueue)
            : base(title, snackbarMessageQueue)
        {
            OpenLinkCommand = new RelayCommand((sender) =>
            {
                if (sender is ILink link && !string.IsNullOrWhiteSpace(link.Adress))
                {
                    System.Diagnostics.Process.Start(link.Adress);
                }
            });
        }
    }
}
