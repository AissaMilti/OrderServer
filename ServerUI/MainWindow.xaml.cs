using System;
using Host;
using Host.Data;
using Host.Helpers;
using Host.Models;
using ServerUI.Host.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
        public ObservableCollection<SocketClient> Connections = SocketHelper.ConnectionsObsverableCollection;

        public MainWindow()
        {
            InitializeComponent();
            var serviceStarter = new ServiceStarter();
            serviceStarter.Start();
            var ip = SocketHelper.GetLocalIPAddress();
            LabelAddress.Content = ip;
            var vm = new MainWindowViewModel
            {
                Orders = Orders,
                SocketClients = Connections
            };
            DataContext = vm;
        }

        private void ListViewOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LabelDishesToPrepare.Content = " ";
            List<string> dishes = new List<string>();
            var order = (Order)ListViewOrders.SelectedItems[0];
            foreach (var item in order.DishIdArray)
            {
                var dish = Context.Dishes.Where(d => d.Id == item).FirstOrDefault();
                dishes.Add(dish.Name);
            }
            LabelDishesToPrepare.Content = $"Order id: {order.CustomerId}\n" +  string.Join($"\n", dishes);
        }

        private void BtnOrderComplete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = (Order)ListViewOrders.SelectedItems[0];
                var client = SocketHelper.Connections.FirstOrDefault(c => c.CustomerId.Contains(order.CustomerId.Value));
                order.Done = true;
                var response = Encoding.GetEncoding(1252).GetBytes("Order " + order.CustomerId.ToString() + " är klart att hämtas!");
                client.Socket.Send(response);

                OrderData.Orders.Remove(order);
                OrderData.OrdersObsverableCollection.Remove(order);
            }
            catch (Exception exception)
            {
               
            }
        }
    }
}
