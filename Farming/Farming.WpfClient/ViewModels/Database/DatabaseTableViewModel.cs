using Farming.WpfClient.Commands;
using Farming.WpfClient.Helpers;
using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace Farming.WpfClient.ViewModels.Database
{
    public abstract class DatabaseTableViewModel<TModel> : PageViewModel, IDatabaseTableViewModel, ICanSort, ICanFilter
        where TModel : class, IValidationViewModel
    {
        private User _user;

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

        protected MemberInfo[] _searchProperies;
        public MemberInfo[] SearchProperties => _searchProperies;

        public MemberInfo SelectedSearchProperty
        {
            get => Get(() => SelectedSearchProperty);
            set => Set(() => SelectedSearchProperty, value);
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

        protected Predicate<object> _searchPredicate = null;

        protected Predicate<object> _filterPredicate = null;

        public ICommand AddCommand { get; }

        public ICommand UpdateCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand ApplySortCommand { get; }

        public ICommand ClearSortCommand { get; }

        public ICommand ApplyFilterCommand { get; }

        public ICommand ClearFilterCommand { get; }

        public ICommand SaveAsCommand { get; }

        public DatabaseTableViewModel(string title, User user, ISnackbarMessageQueue snackbarMessageQueue)
            : this(title, user, snackbarMessageQueue, null)
        {
            
        }

        public DatabaseTableViewModel(string title, User user, ISnackbarMessageQueue snackbarMessageQueue, IEnumerable<TModel> models)
            : base(title, snackbarMessageQueue)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));

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

            ApplyFilterCommand = new RelayCommand(sender => ApplyFilter());

            ClearFilterCommand = new RelayCommand(sender => ClearFilter());

            SaveAsCommand = new RelayCommand(sender =>
            {
                if (Models == null || !Models.Any()) return;

                using (var saveFileDialog = new SaveFileDialog()
                {
                    Filter = "PDF|*.pdf",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    FilterIndex = 1
                })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            SaveAs(saveFileDialog.FileName);
                        }
                        catch (Exception)
                        {
                            Enqueue("Непредвиденая ошибка при сохранении файла.");
                        }
                    }
                }
            });

            if (models != null)
            {
                Models = new ObservableCollection<TModel>(models);
            }
            else
            {
                Models = new ObservableCollection<TModel>();
            }

            var stringType = typeof(string);
            _sortProperies = typeof(TModel)
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType.IsPrimitive || p.PropertyType == stringType || p.PropertyType.IsValueType)
                .ToArray();

            _searchProperies = _sortProperies;

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

        private FarmingEntities GetFarmingEntities()
        {
            switch (_user.UserType.Name)
            {
                case "Менеджер":

                    return new FarmingEntities("name=FarmingManager");

                case "Администратор":

                    return new FarmingEntities();

                default:

                    //Пользователь неидентифицирован
                    return null;
            }
        }

        private async Task RunDbCommandAsync(Func<FarmingEntities, Task> func)
        {
            var db = GetFarmingEntities();

            if (db == null)
            {
                Enqueue("Пользователь неидентифицирован.");
            }
            else
            {
                using (db)
                {
                    await func(db);
                }
            }
        }

        public void ClearData()
        {
            if (Models.Any())
                Models.Clear();

            SelectedModel = null;

            ClearFilter();
            ClearSearch();
            ClearSort();
        }

        public override Task UpdateAsync()
        {
            if (Models != null && Models.Any())
                Models.Clear();

            return RunDbCommandAsync(async db => 
            {
                if (IsBusy) return;

                IsBusy = true;

                await Task.Delay(800);

                try
                {
                    await UpdateAsync(db);
                }
                catch (Exception)
                {
                    Enqueue("Ошибка при обновлении данных.");
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }

        public Task AddAsync(TModel model)
        {
            if (model == null || !model.Valid())
                return Task.CompletedTask;

            return RunDbCommandAsync(async db =>
            {
                try
                {
                    await AddAsync(db, model);

                    Enqueue("Добавлено.");
                }
                catch (Exception)
                {
                    Enqueue("Ошибка при добавлении данных.");
                }
            });
        }

        public Task UpdateAsync(TModel model)
        {
            if (model == null || !model.Valid())
                return Task.CompletedTask;

            return RunDbCommandAsync(async db =>
            {
                try
                {
                    await UpdateAsync(db, model);

                    Enqueue("Обновлено.");
                }
                catch (Exception)
                {
                    Enqueue("Ошибка при обновлении данных.");
                }
            });
        }

        public Task DeleteAsync(TModel model)
        {
            if (model == null || !model.Valid())
                return Task.CompletedTask;

            return RunDbCommandAsync(async db =>
            {
                try
                {
                    await DeleteAsync(db, model);

                    Enqueue("Запись удалена.");
                }
                catch (Exception)
                {
                    Enqueue("Ошибка при удалении данных.");
                }
            });
        }

        protected abstract Task UpdateAsync(FarmingEntities db);

        protected abstract Task AddAsync(FarmingEntities db, TModel model);

        protected abstract Task UpdateAsync(FarmingEntities db, TModel model);

        protected abstract Task DeleteAsync(FarmingEntities db, TModel model);

        public virtual void SaveAs(string fileName)
        {

        }

        public virtual void ApplySort(SortType sortType, string value)
        {
            if (string.IsNullOrWhiteSpace(value) || IsBusy) return;

            IsBusy = true;

            if (_modelsView.SortDescriptions.Any())
                _modelsView.SortDescriptions.Clear();

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

            IsBusy = false;
        }

        public virtual void ClearSort()
        {
            if (IsBusy) return;

            if (_modelsView.SortDescriptions.Any())
            {
                IsBusy = true;

                _modelsView.SortDescriptions.Clear();
            }

            SelectedSortProperty = null;
            SelectedSortType = null;

            IsBusy = false;
        }

        public override void Search(object sender)
        {
            if (SelectedSearchProperty == null || IsBusy) return;

            IsBusy = true;

            if (sender is string text)
            {
                text = text.ToLower();

                if (string.IsNullOrWhiteSpace(text))
                {
                    if (_searchPredicate != null)
                    {
                        _searchPredicate = null;

                        if (_filterPredicate != null)
                        {
                            _modelsView.Filter = _filterPredicate;
                        }
                        else
                        {
                            _modelsView.Filter = null;
                        }
                    }
                }
                else
                {
                    _searchPredicate = model =>
                    {
                        return model
                            .GetType()
                            .GetProperty(SelectedSearchProperty.Name)
                            .GetValue(model)
                            .ToString()
                            .ToLower()
                            .Contains(text);
                    };

                    _modelsView.Filter = model => _searchPredicate.Invoke(model) &&
                            (_filterPredicate == null ? true : _filterPredicate.Invoke(model));
                }
            }

            IsBusy = false;
        }

        public override void ClearSearch()
        {
            if (IsBusy) return;

            IsBusy = true;

            _searchPredicate = null;

            if (_filterPredicate != null)
            {
                _modelsView.Filter = _filterPredicate;
            }
            else
            {
                _modelsView.Filter = null;
            }

            SelectedSearchProperty = null;
            SearchText = null;

            IsBusy = false;
        }

        public virtual void ApplyFilter(params string[] by)
        {

        }

        public virtual void ClearFilter()
        {

        }
    }
}
