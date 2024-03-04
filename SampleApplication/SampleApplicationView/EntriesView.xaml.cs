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
using SampleApplicationViewModel;

namespace SampleApplicationView
{
    /// <summary>
    /// Interaction logic for EntriesView.xaml
    /// </summary>
    public partial class EntriesView : UserControl
    {
        public EntriesView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setting the new entry parent's account to the one in DataContext.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            //(e.NewItem as EntryViewModel).AccountViewModel = DataContext as AccountViewModel;
        }
    }
}
