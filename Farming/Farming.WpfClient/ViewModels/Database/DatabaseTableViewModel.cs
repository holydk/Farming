using Farming.WpfClient.Commands;
using Farming.WpfClient.Helpers;
using Farming.WpfClient.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Farming.WpfClient.ViewModels.Database
{
    public abstract class DatabaseTableViewModel<TModel> : PageViewModel, IDatabaseTableViewModel, ICanSort, ICanFilter
        where TModel : class
    {
        protected ICollectionView _modelsView => CollectionViewSource.GetDefaultView(Models);

        protected MemberInfo[] _sortProperies;
        public MemberInfo[] SortProperties => _sortProperies;

        public MemberInfo SelectedSortProperty
        {
            get => Get(() => SelectedSortProperty);
            set => Set(() => SelectedSortProperty, value);
        }

        public SortType[] SortTypes { get; }

        public SortType? SelectedSortType
        {
            get => Get(() => SelectedSortType, null);
            set => Set(() => SelectedSortType, value);
        }

        public ObservableCollection<TModel> Models
        {
            get => Get(() => Models);
            private set => Set(() => Models, value);
        }

        public TModel SelectedModel
        {
            get => Get(() => SelectedModel);
            set => Set(() => SelectedModel, value);
        }

        public ICommand AddCommand { get; }

        public ICommand UpdateCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand ApplySortCommand { get; }

        public ICommand ClearSortCommand { get; }

        public DatabaseTableViewModel(string title)
            : base(title)
        {
            AddCommand = new RelayCommand(
                async sender => await RunActionAsync(sender, AddAsync));

            UpdateCommand = new RelayCommand(
                async sender => await RunActionAsync(sender, UpdateAsync));

            DeleteCommand = new RelayCommand(
                async sender => await RunActionAsync(sender, DeleteAsync));

            ApplySortCommand = new RelayCommand(sender =>
            {
                if (SelectedSortType.HasValue && SelectedSortProperty != null)
                {
                    ApplySort(SelectedSortType.Value, SelectedSortProperty.Name);
                }
            });

            ClearSortCommand = new RelayCommand(sender => ClearSort());

            Models = new ObservableCollection<TModel>();

            var stringType = typeof(string);
            _sortProperies = typeof(TModel)
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType.IsPrimitive || p.PropertyType == stringType)
                .ToArray();

            var sortT = typeof(SortType);
            SortTypes = Enum
                .GetValues(sortT).OfType<SortType>()
                .OrderBy(d => sortT.GetField(d.ToString()).ToDescription(), StringComparer.InvariantCultureIgnoreCase)
                .ToArray();
        }

        private async Task RunActionAsync(object param, Func<TModel, Task> action)
        {
            if (param is TModel model && model != null)
            {
                await action(model);
            }
        }

        protected abstract Task AddAsync(TModel model);

        protected abstract Task UpdateAsync(TModel model);

        protected abstract Task DeleteAsync(TModel model);

        public virtual void ApplySort(SortType sortType, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;

            ClearSort();

            switch (sortType)
            {
                case SortType.Ascending:

                    _modelsView.SortDescriptions.Add(new SortDescription(value, ListSortDirection.Ascending));

                    break;

                case SortType.Descending:

                    _modelsView.SortDescriptions.Add(new SortDescription(value, ListSortDirection.Descending));

                    break;

                default:
                    break;
            }
        }

        public virtual void ClearSort()
        {
            if (_modelsView.SortDescriptions.Any())
                _modelsView.SortDescriptions.Clear();
        }

        public virtual void ApplyFilter(params string[] by)
        {

        }

        public virtual void ClearFilter()
        {

        }
    }
}
