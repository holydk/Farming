using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Farming.WpfClient.Helpers
{
    public class MemberInfoToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MemberInfo memberInfo)
            {
                return memberInfo.ToDescription();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
