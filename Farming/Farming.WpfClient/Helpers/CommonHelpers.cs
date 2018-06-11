using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
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

        /// <summary>
        /// Дешифруем <see cref="SecureString"/> к string
        /// </summary>
        /// <param name="secureString"></param>
        /// <returns></returns>
        public static string Unsecure(this SecureString secureString)
        {
            if (secureString == null)
                return string.Empty;

            var unmanagedString = IntPtr.Zero;

            try
            {
                // Дешифруем
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);

                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                // Очистить память
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static void AddTitleTable(this PdfPTable table, string text, Font font, int colSpan = 1)
        {
            AddCell(table, text, font, 0, 0, 0, 10, 10, colSpan, 1.5F);
        }

        public static void AddHeaderColumn(this PdfPTable table, string text, Font font, int colSpan = 1, int alignment = 0, float paddingLeft = 5)
        {
            AddCell(table, text, font, alignment, paddingLeft, 5, 10, 10, colSpan);
        }

        public static void AddCell(this PdfPTable table, string text, Font font, int alignment = 0, float paddingLeft = 5, float paddingRight = 5, float paddingTop = 5, float paddingBottom = 5, int colSpan = 1, float borderWidthBottom = 0)
        {
            table.AddCell(new PdfPCell()
            {
                Phrase = new Phrase(text, font),
                Border = 0,
                PaddingLeft = paddingLeft,
                PaddingRight = paddingRight,
                PaddingTop = paddingTop,
                PaddingBottom = paddingBottom,
                HorizontalAlignment = alignment,
                Colspan = colSpan,
                BorderWidthBottom = borderWidthBottom
            });
        }
    }
}
