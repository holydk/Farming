﻿using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using Farming.WpfClient.Commands;
using Farming.WpfClient.Models;
using Farming.WpfClient.ViewModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Farming.WpfClient.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView<MainViewModel>
    {
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand CloseCommand { get; }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.RegisterAttached("IsBusy", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        public static void SetIsBusy(DependencyObject obj, bool value)
        {
            obj.SetValue(IsBusyProperty, value);
        }

        public static bool GetIsBusy(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsBusyProperty);
        }

        // Using a DependencyProperty as the backing store for IsOpenInformationBox.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenInformationBoxProperty = DependencyProperty.Register("IsOpenInformationBox", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        public bool IsOpenInformationBox
        {
            get { return (bool)GetValue(IsOpenInformationBoxProperty); }
            set { SetValue(IsOpenInformationBoxProperty, value); }
        }

        private int _currentNavIndex;

        public MainViewModel ViewModel
        {
            get => DataContext as MainViewModel;
            set => DataContext = value;
        }

        public MainWindow(MainViewModel viewModel, SnackbarMessageQueue snackbarMessageQueue)
        {
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            SetIsBusy(this, true);

            InitializeComponent();

            MainSnackbar.MessageQueue = snackbarMessageQueue ?? throw new ArgumentNullException(nameof(snackbarMessageQueue));

            MinimizeCommand = new RelayCommand((sender) => WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand((sender) => WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand((sender) => Close());

            RoutedEventHandler loaded = null;
            loaded = async (obj, e) =>
            {
                Loaded -= loaded;

                var PART_Header = (Border)Template.FindName("PART_Header", this);

                if (PART_Header != null)
                    PART_Header.Background = (SolidColorBrush)Application.Current.Resources["MaterialDesignPaper"];

                var mainContent = ((ContentPresenter)Template.FindName("mainWindowContent", this)).Content as Grid;              

                if (mainContent != null)
                {
                    await Task.Run(() => Thread.Sleep(500)).ContinueWith(async (t) =>
                    {
                        var waitPageIcon = (TransitioningContent)mainContent.Children[0];

                        if (waitPageIcon != null)
                            waitPageIcon.Visibility = Visibility.Visible;

                        await Task.Delay(500);
                    }, TaskScheduler.FromCurrentSynchronizationContext())
                    .ContinueWith((t) =>
                    {
                        var waitPageTitle = (TransitioningContent)mainContent.Children[1];

                        if (waitPageTitle != null)
                            waitPageTitle.Visibility = Visibility.Visible;

                    }, TaskScheduler.FromCurrentSynchronizationContext());  
                }              

                var navListBox = (ListBox)Template.FindName("navListBox", this);

                if (navListBox != null)
                {                   
                    navListBox.SelectionChanged += NavListBox_SelectionChanged;
                    navListBox.PreviewMouseLeftButtonUp += NavListBox_PreviewMouseLeftButtonUp;
                    navListBox.SelectedIndex = 0;
                }              

                await Task.Delay(5000);

                ViewModel.Navigation.Navigated += (_obj, _e) =>
                {                   
                    if (_e.NavigationState == NavigationState.GoForward)
                    {
                        var pageViewModel = (IPageViewModel)ViewModel.Navigation.Content.DataContext;

                        if (pageViewModel == null ||
                            pageViewModel.Title == mainTitle.Text)
                            return;

                        var chevronRight = new PackIcon()
                        {
                            Kind = PackIconKind.ChevronRight,
                            Width = 20,
                            Height = 16,
                            Foreground = (SolidColorBrush)Application.Current.Resources["PrimaryHueMidForegroundBrush"]
                        };

                        var uiContainer = new InlineUIContainer(chevronRight)
                        {
                            BaselineAlignment = BaselineAlignment.Center
                        };

                        var pageTitle = new Run(pageViewModel.Title)
                        {
                            Tag = ViewModel.Navigation.Content.GetType()
                        };

                        navTextBox.Inlines.AddRange(new Inline[]
                        {
                            uiContainer,
                            pageTitle
                        });
                    }
                    else if (_e.NavigationState == NavigationState.GoBack)
                    {
                        ClearNavTextBox(_currentNavIndex);
                    }
                };

                SetIsBusy(this, false);

                await Task.Delay(2500).ContinueWith(t =>
                {
                    //note you can use the message queue from any thread, but just for the demo here we 
                    //need to get the message queue from the snackbar, so need to be on the dispatcher
                    MainSnackbar.MessageQueue.Enqueue($"Приветствуем, {viewModel.User.LastName} {viewModel.User.FirstName} {viewModel.User.MiddleName}!");
                }, TaskScheduler.FromCurrentSynchronizationContext());
            };

            Loaded += loaded;
        }

        private void NavListBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private void NavListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            // Single
            var item = e.AddedItems[0];

            if (item is INavigationItem navItem)
            {
                // maintTitle заголовок главной страницы
                // необходимо знать ее Тип чтобы потом 
                // вернутся на эту страницу с дочерней страницы
                // Пример : МайнТайтл > Тайтл1 > Тайтл2 и тд
                // Если мы находимся на дочерней странице Тайтл2, то 
                // при нажатии на МайнТайтл должны перейти
                // на главную страницу при этом Тайтл1 и Тайтл2
                // удаляются из навиг. меню.
                mainTitle.Tag = navItem.ViewType;
                mainTitle.Text = navItem.Title;

                ViewModel.GoToCommand.Execute(navItem.ViewType);
            }

            ClearNavTextBox();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            var containerBorder = (Border)Template.FindName("PART_Container", this);

            if (containerBorder == null) return;

            if (WindowState == WindowState.Maximized)
            {
                // Make sure window doesn't overlap with the taskbar.

                //var screen = System.Windows.Forms.Screen.FromHandle(handle);
                //if (screen.Primary)
                {
                    containerBorder.Padding = new Thickness(
                        SystemParameters.WorkArea.Left + 7,
                        SystemParameters.WorkArea.Top + 7,
                        (SystemParameters.PrimaryScreenWidth - SystemParameters.WorkArea.Right) + 5,
                        (SystemParameters.PrimaryScreenHeight - SystemParameters.WorkArea.Bottom) - 40);
                }
            }
            else
            {
                containerBorder.Padding = new Thickness(0, 0, 0, 0);
            }

            base.OnStateChanged(e);
        }

        private void Run_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Run run)
            {
                _currentNavIndex = navTextBox.Inlines.ToList().IndexOf(run);

                ViewModel.GoBackToCommand.Execute(run.Tag);
            }
        }

        private void ClearNavTextBox(int before = 0)
        {
            while (navTextBox.Inlines.Count - 1 > before)
            {
                navTextBox.Inlines.Remove(navTextBox.Inlines.LastInline);
            }
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            if (IsOpenInformationBox)
                scrollInfoBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;                          
        }

        private void infoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsOpenInformationBox)
                scrollInfoBox.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }

        private void SearchTextBox_Search(string text)
        {
            var content = ViewModel.Navigation.Content;

            if (content != null && content.DataContext is IPageViewModel pageVm)
            {
                pageVm.Search(text);
            }
        }
    }
}
