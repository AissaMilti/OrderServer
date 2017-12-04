using Host.Models;
using System.Collections.ObjectModel;

namespace ServerUI.Host.Models
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<SocketClient> SocketClients { get; set; }
    }
}
