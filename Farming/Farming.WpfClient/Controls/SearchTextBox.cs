﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Farming.WpfClient.Controls
{
    /// <summary>
    /// При использовании возможно неожиданное фиаско
    /// </summary>
    public class SearchTextBox : TextBox
    {
        protected Button _searchBtn;
        
        protected bool _isAnimated;

        protected double _defaultContentWidth;

        protected double _minimizeWidth;

        protected bool _isInit = false;

        public event Action<string> Search;

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public SearchTextBox()
        {
            RoutedEventHandler loaded = null;
            loaded = (obj, e) =>
            {
                Loaded -= loaded;

                //if (IsOpen)
                    //_defaultContentWidth = Width;
            };

            Loaded += loaded;
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(SearchTextBox), new PropertyMetadata(false, IsOpenChangedCallback));

        private static void IsOpenChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SearchTextBox searchTb)
            {
                if (e.NewValue is bool isOpen)
                {
                    if (isOpen)
                    {
                        BeginAnimation(searchTb, searchTb._defaultContentWidth);
                    }
                    else
                    {
                        var searchBtn = searchTb._searchBtn;

                        if (searchBtn != null)
                        {
                            BeginAnimation(searchTb, searchTb._minimizeWidth);
                        }
                    }
                }
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            if (!sizeInfo.WidthChanged || _isAnimated || _searchBtn == null) return;

            if (!_isInit)
            {
                _isInit = true;

                _defaultContentWidth = sizeInfo.NewSize.Width;
            }

            _minimizeWidth = _searchBtn.ActualWidth;

            if (!IsOpen)
            {
                BeginAnimation(this, _minimizeWidth, new TimeSpan(0));
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
           
            _searchBtn = (Button)Template.FindName("searchBtn", this);
            var _closeBtn = (Button)Template.FindName("closeBtn", this);
            var _mainContent = (DockPanel)Template.FindName("mainContent", this);

            if (_searchBtn != null)
            {
                _searchBtn.Click -= SearchBtn_Click;
                _searchBtn.Click += SearchBtn_Click;
            }

            if (_closeBtn != null)
            {
                _closeBtn.Click -= CloseBtn_Click;
                _closeBtn.Click += CloseBtn_Click;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Enter)
                OnSearch();
            else if (e.Key == Key.Escape)
                Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Close()
        {
            IsOpen = false;
            Keyboard.ClearFocus();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsOpen)
                OnSearch();
            else
                IsOpen = true;

            Focus();
        }

        protected virtual void OnSearch() => Search?.Invoke(Text);

        private static void BeginAnimation(SearchTextBox searchTextBox, double toWidth, TimeSpan? time = null)
        {
            var widthAnimation = new DoubleAnimation(toWidth, time ?? new TimeSpan(0, 0, 0, 0, 200))
            {
                EasingFunction = new SineEase()
                {
                    EasingMode = EasingMode.EaseInOut
                }
            };

            widthAnimation.Completed += (sender, args) =>
            {
                widthAnimation.BeginAnimation(WidthProperty, null);
                searchTextBox._isAnimated = false;
            };

            searchTextBox._isAnimated = true;
            searchTextBox.BeginAnimation(WidthProperty, widthAnimation);
        }
    }
}
