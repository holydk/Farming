using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Farming.WpfClient.Models
{
    public class ActionItem : CommandItem, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    PropertyChangedEventHandler();
                }
            }
        }

        public PackIconKind DeactiveIconKind { get; }

        public ActionItem(string title, PackIconKind iconKind, PackIconKind deactiveIconKind, ICommand command)
            : base(title, iconKind, command)
        {
            DeactiveIconKind = deactiveIconKind;
        }

        public void PropertyChangedEventHandler([CallerMemberName]string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
