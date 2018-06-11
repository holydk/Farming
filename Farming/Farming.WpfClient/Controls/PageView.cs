using Farming.WpfClient.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Farming.WpfClient.Controls
{
    public abstract class PageView : UserControl
    {
        public static readonly DependencyProperty ActionContentProperty =
            DependencyProperty.Register("ActionContent", typeof(object), typeof(PageView), new PropertyMetadata(null));

        public object ActionContent
        {
            get { return GetValue(ActionContentProperty); }
            set { SetValue(ActionContentProperty, value); }
        }

        public List<ICommandItem> ActionItems { get; }

        public PageView()
        {
            ActionItems = new List<ICommandItem>();
        }

        private void DeactivatedItemsBesides(ActionItem item)
        {
            var items = ActionItems.OfType<ActionItem>();

            if (items != null && items.Any())
            {
                foreach (var _item in items)
                {
                    if (_item.Title != item.Title)
                    {
                        _item.IsActive = false;
                    }
                }
            }
        }

        protected virtual void UpdateContent(ActionItem currentActionItem, object content)
        {
            if (currentActionItem.IsActive)
            {
                DeactivatedItemsBesides(currentActionItem);

                ActionContent = content;
            }
            else
            {
                ActionContent = null;
            }
        }
    }
}
