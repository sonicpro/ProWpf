using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Model
{
    public class Model
    {
        private IPAddress ipAddress;

        public ObservableCollection<Port> Ports
        {
            get;
            set;
        } = new ObservableCollection<Port>();

        // TODO: This constructor is flawed. ipAddress could still not be initialized.
        public Model(string machineAddress)
        {
            if (!IPAddress.TryParse(machineAddress, out ipAddress))
            {
                // Assume machine name.
                IPHostEntry hostEntry = Dns.GetHostEntry(machineAddress);
                if (!hostEntry.AddressList.Any())
                {
                    ipAddress = hostEntry.AddressList[0];
                }
            }
        }

        public void TestPorts()
        {
            for (var port = 60000; port < 60001; port++)
            {
                TestPort(port);
            }
        }

        private void TestPort(int port)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(ipAddress, port);
                if (socket.Connected)
                {
                    Ports.Add(new Port(port, false));
                }
            }
            catch (SocketException ex)
            {
                Ports.Add(new Port(port, true));
            }
            catch
            {
                Ports.Add(new Port(port, null));
            }
            finally
            {
                socket.Close();
            }
        }
    }
}
