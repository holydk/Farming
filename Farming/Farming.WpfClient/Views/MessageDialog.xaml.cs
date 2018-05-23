using System.Windows.Controls;

namespace Farming.WpfClient.Views
{
    /// <summary>
    /// Логика взаимодействия для MessageDialog.xaml
    /// </summary>
    public partial class MessageDialog : UserControl
    {
        public MessageDialog(string action)
        {
            InitializeComponent();

            ActionTb.Text = action;
        }
    }
}
