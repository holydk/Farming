using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace Farming.WpfClient.Helpers
{
    public static partial class CommonHelpers
    {
        public static void ExecuteOnLoaded(this FrameworkElement element, Action action)
        {
            RoutedEventHandler handler = null;
            handler = (obj, e) =>
            {
                element.Loaded -= handler;

                action?.Invoke();
            };

            element.Loaded += handler;
        }

        public static string ToDescription(this MemberInfo memberInfo) =>
            memberInfo.GetCustomAttribute<DescriptionAttribute>(false)?.Description ?? string.Empty;
    }
}
