using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewAssignment2KIT206
{
    /// <summary>
    /// Interaction logic for FilteringByLevelControl.xaml
    /// </summary>
    public partial class FilteringByLevelControl : UserControl
    {
        public FilteringByLevelControl()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
            {
                MessageBox.Show("Dropdown list used to select: " + e.AddedItems[0]);
                MainWindow mainWindow = new MainWindow();
                mainWindow.controller.Filter((EmploymentLevel)e.AddedItems[0]);
            }
        }
    }
}
