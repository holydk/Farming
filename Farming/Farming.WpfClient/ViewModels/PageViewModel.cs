using Farming.WpfClient.Commands;
using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Farming.WpfClient.ViewModels
{
    public class PageViewModel : ValidationViewModelBase, IPageViewModel
    {
        private ISnackbarMessageQueue _snackbarMessageQueue;

        public string Title { get; }

        public ICommand SearchCommand { get; }

        public ICommand ClearSearchCommand { get; }

        public string SearchText
        {
            get => Get(() => SearchText);
            set => Set(() => SearchText, value);
        }

        public bool IsBusy
        {
            get => Get(() => IsBusy);
            set => Set(() => IsBusy, value);
        }

        public PageViewModel(string title, ISnackbarMessageQueue snackbarMessageQueue)
        {
            Title = title;
            _snackbarMessageQueue = snackbarMessageQueue;

            SearchCommand = new RelayCommand(sender => Search(sender));
            ClearSearchCommand = new RelayCommand(sender => ClearSearch());
        }

        protected void Enqueue(string msg)
        {
            _snackbarMessageQueue?.Enqueue(msg);
        }

        public virtual void Search(object sender)
        {
            
        }

        public virtual void ClearSearch()
        {
            SearchText = null;
        }

        public virtual Task UpdateAsync()
        {
            return Task.CompletedTask;
        }
    }
}
