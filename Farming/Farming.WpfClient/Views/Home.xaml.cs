﻿using Farming.WpfClient.ViewModels;
using System.Windows.Controls;

namespace Farming.WpfClient.Views
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : UserControl, IView<HomeViewModel>
    {
        public HomeViewModel ViewModel
        {
            get => DataContext as HomeViewModel;
            set => DataContext = value;
        }

        public Home(HomeViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
        }
    }
}
