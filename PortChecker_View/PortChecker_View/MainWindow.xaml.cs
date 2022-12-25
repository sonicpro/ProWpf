using System.Net.Sockets;
using System.Windows;

namespace PortChecker_View
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

        private void CheckPortClick(object _, RoutedEventArgs __)
        {
            var portNumberInt = -1;
            if (int.TryParse(portNumber.Text, out portNumberInt))
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    socket.Connect(System.Net.Dns.GetHostName(), portNumberInt);
                    if (socket.Connected)
                    {
                        MessageBox.Show("This port is listening.");
                    }
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        MessageBox.Show("This port is not listening.");
                    }
                    else
                    {
                        throw;
                    }
                }
                finally
                {
                    socket.Close();
                }
            }
            else
            {
                MessageBox.Show("Sorry, the port number entered is not valid.");
            }
        }
    }
}
