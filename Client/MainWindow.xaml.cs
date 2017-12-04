using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;
using Client.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BtnOrder.IsEnabled = false;
            BtnChose.IsEnabled = false;
            BtnRemov.IsEnabled = false;
        }

        private ObservableCollection<Dish> Dishes = new ObservableCollection<Dish>();
        TcpClient client;
        NetworkStream ns;

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            Connect();
        }

        IPEndPoint RemoteEndPoint()
        {
            var portParsed = Int32.Parse(TxtboxPort.Text);
            var ipAddressParsed = IPAddress.Parse(TxtboxIPAddress.Text);
            var endPoint = new IPEndPoint(ipAddressParsed, portParsed);

            return endPoint;
        }

        private void Connect()
        {
            
            try
            {
                var endpoint = RemoteEndPoint();

                var data = new Byte[1024];
                client = new TcpClient();
                Application.Current.Dispatcher.Invoke(new Action(() => { Message.Content = "  " ; }));
                client.Connect(endpoint);
                ns = client.GetStream();
                var recv = ns.Read(data, 0, data.Length);
                var response = Encoding.ASCII.GetString(data, 0, recv);
                var dishes = JsonConvert.DeserializeObject<List<Dish>>(response);
                foreach (var dish in dishes)
                {
                    Dishes.Add(dish);
                }
                DataContext = dishes;
                var task1 = new Task(() => ClientRecieve());
                task1.Start();
                
            }
            catch (Exception e)
            {
                var message = " Ip address or Port number is wrong";
                Application.Current.Dispatcher.Invoke(new Action(() => { Message.Content = message; }));
            }
        }
      
        public List<Dish> GetDish()
        {

        
         List<Dish> selectedDishes = new List<Dish>();
            foreach (var item in ListViewOrders.Items)
            {
                var dish = item as Dish;
                if (dish != null)
                {
                    selectedDishes.Add(dish);
                }
            }
            return selectedDishes;
        }
        void ClientSend()
        {


         

            var dishIds = GetDish().Select(d => d.Id).ToArray();


            var customerId = Guid.NewGuid().ToString();

            var order = new Order
            {                
                DishIdArray = dishIds,
                Done = false
            };

            var jsonOrder = JsonConvert.SerializeObject(order);

            var bytesToSend = Encoding.ASCII.GetBytes(jsonOrder);

            try
            {
                client.Client.Send(bytesToSend);
            }
            catch (Exception)
            {


            }
        }

        private void ClientRecieve()
        {
            while (true)
            {
                var data = new byte[1024];
                var recv = ns.Read(data, 0, data.Length);
                var message = Encoding.GetEncoding(1252).GetString(data, 0, recv);
                Application.Current.Dispatcher.Invoke(new Action(() => { Message.Content = $"{message}"; }));
            }
        }

        private void BtnChose_OnClick(object sender, RoutedEventArgs e)
        {
            var dish = ListViewDishes.SelectedItem;
            ListViewOrders.Items.Add(dish);            
            ListViewDishes.SelectedItem = null;
            BtnChose.IsEnabled = false;
            BtnOrder.IsEnabled = true;
        }

        private void BtnOrder_OnClick(object sender, RoutedEventArgs e)
        {
            ClientSend();
            ListViewOrders.Items.Clear();
            BtnOrder.IsEnabled = false;
        }

        private void ListViewDishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnChose.IsEnabled = true;
        }

        private void ListViewOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnRemov.IsEnabled = true;
        }

        private void BtnRemov_Click(object sender, RoutedEventArgs e)
        {
            var dish = ListViewOrders.SelectedItem;
            ListViewOrders.Items.Remove(dish);
            BtnRemov.IsEnabled = false;
            if(ListViewOrders.Items.IsEmpty)
            {
                BtnOrder.IsEnabled = false;
            }
        }        
    }
}


