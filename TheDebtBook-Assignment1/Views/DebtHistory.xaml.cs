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
using System.Windows.Shapes;

namespace TheDebtBook_Assignment1.Views
{
    /// <summary>
    /// Interaction logic for DebtHistory.xaml
    /// </summary>
    public partial class DebtHistory : Window
    {
        public DebtHistory()
        {
            InitializeComponent();
        }

        private void HistoryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
