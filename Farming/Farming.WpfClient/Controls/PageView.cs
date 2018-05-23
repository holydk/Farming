using Farming.WpfClient.Models;
using System.Collections.Generic;
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

        public List<ActionItem> ActionItems { get; }

        public PageView()
        {
            ActionItems = new List<ActionItem>();
        }

        private void DeactivatedItemsBesides(ActionItem item)
        {
            foreach (var _item in ActionItems)
            {
                if (_item.Title != item.Title)
                {
                    _item.IsActive = false;
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
