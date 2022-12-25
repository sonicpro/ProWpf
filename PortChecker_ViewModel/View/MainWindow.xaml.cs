using System.Windows;
using PortCheckerModel = Model.Model;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void On_CheckPortsClick(object _, RoutedEventArgs __)
        {
            var portChecker = new PortCheckerModel(machineNameOrAddress.Text);
            ports.ItemsSource = portChecker.Ports;
            portChecker.TestPorts();
        }
    }
}
