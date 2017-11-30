﻿using Host;
using Host.Data;
using Host.Helpers;
using Host.Models;
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
            List<string> dishes = new List<string>();
            var Order = (Order)ListViewOrders.SelectedItems[0];
            foreach (var item in Order.DishIdArray)
            {
                var dish = Context.Dishes.Where(d => d.Id == item).FirstOrDefault();
                dishes.Add(dish.Name);
            }
            LabelDishesToPrepare.Content = string.Join(",", dishes);
        }

        private void BtnOrderComplete_Click(object sender, RoutedEventArgs e)
        {
            var order = (Order)ListViewOrders.SelectedItems[0];
            //var endpoint = OrderData.OrderClient.FirstOrDefault(o => o.CustomerId.Equals(Order.CustomerId)).EndPoint;
            var client = SocketHelper.Connections.FirstOrDefault(c => c.CustomerId.Contains(order.CustomerId));
            order.Done = true;

            var response = Encoding.UTF8.GetBytes(order.CustomerId);

            client.Socket.Send(response);
            
        }        

    

    }
}