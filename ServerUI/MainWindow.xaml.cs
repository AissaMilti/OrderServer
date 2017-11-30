using OrderServer.Host;
using OrderServer.Host.Data;
using OrderServer.Host.Helpers;
using OrderServer.Host.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ServerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Order> Orders = new ObservableCollection<Order>();
        

        public MainWindow()
        {            
            InitializeComponent();
            var serviceStarter = new ServiceStarter();
            serviceStarter.Start();
            var ip = SocketHelper.GetLocalIPAddress();
            LabelAddress.Content = ip;
            //int[] arr = {1,2};
            //int[] arr2 = { 1 };
            //Orders.Add(new Order() { Done = false, CustomerId = "212415faaf", DishIdArray = arr });
            //Orders.Add(new Order() { Done = false, CustomerId = "12515", DishIdArray = arr2 });
            DataContext = Orders;
            var task = new Task(UpdateData);
            task.Start();
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
        private void UpdateData()
        {
            while (true)
            {
                foreach (var order in OrderData.Orders)
                {
                    if (Orders.Contains(order))
                    {
                        continue;
                    }
                    Orders.Add(order);
                }
            }
        }
    }
}
