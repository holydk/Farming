using System.Windows.Input;

namespace Farming.WpfClient.ViewModels.Database
{
    public interface IDatabaseTableViewModel : IPageViewModel, IValidationViewModel
    {
        ICommand AddCommand { get; }

        ICommand UpdateCommand { get; }

        ICommand DeleteCommand { get; }
    }
}
