using Host;
using Host.Data;
using Host.Helpers;
using Host.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ServerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<Order> Orders = OrderData.OrdersObsverableCollection;



        public MainWindow()
        {
            
            InitializeComponent();
            var serviceStarter = new ServiceStarter();
            serviceStarter.Start();
            var ip = SocketHelper.GetLocalIPAddress();
            LabelAddress.Content = ip;           
            DataContext = Orders;       
           
        }

        private void ListViewOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Order = (Order)ListViewOrders.SelectedItems[0];
            var dishes = Context.Dishes.Where(d => Order.DishIdArray.Contains(d.Id)).Select(d => d.Name);
            LabelDishesToPrepare.Content = string.Join(",", dishes);
        }

        private void BtnOrderComplete_Click(object sender, RoutedEventArgs e)
        {
            //var Order = (Order)ListViewOrders.SelectedItems[0];
            //SocketHelper.Connections.First().Send()
        }        

    

    }
}
