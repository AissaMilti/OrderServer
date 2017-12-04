using System.Collections.ObjectModel;
using Host.Models;

namespace ServerUI.Host.Models
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<SocketClient> SocketClients { get; set; }
    }
}
