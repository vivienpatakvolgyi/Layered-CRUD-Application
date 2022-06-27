using D2P0JX_SG1_21_22_2.WpfClient.Models;
using D2P0JX_SG1_21_22_2.WpfClient.ViewModels;
using System.Windows;

namespace D2P0JX_SG1_21_22_2.WpfClient
{
    /// <summary>
    /// Interaction logic for PizzaEditorWindow.xaml
    /// </summary>
    public partial class PizzaEditorWindow : Window
    {
        public PizzaModel Pizza { get; set; }
        bool enableEdit;
        public PizzaEditorWindow(PizzaModel pizza = null, bool enableEdit = true)
        {
            InitializeComponent();
            Pizza = pizza == null
                ? new PizzaModel()
                : new PizzaModel(pizza);
            this.enableEdit = enableEdit;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var vm = (PizzaEditorVM)Resources["VM"];
            vm.CurrentPizza = Pizza;
            vm.EditEnabled = enableEdit;
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            if (enableEdit)
            {
                DialogResult = true;
            }
            else
            {
                Close();
            }
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            if (enableEdit)
            {
                DialogResult = false;
            }
            else
            {
                Close();
            }
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
