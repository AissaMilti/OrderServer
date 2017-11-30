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

        void Connect()
        {
            var endpoint = RemoteEndPoint();               

            var data = new Byte[1024];
            client = new TcpClient();
            try
            {
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
            catch (Exception)
            {
                    
            }
        }

        void ClientSend()
        {
            var selectedDishes = new List<Dish>();
            
            foreach (var item in ListViewOrders.Items)
            {
                var dish = item as Dish;
                if(dish != null)
                {
                    selectedDishes.Add(dish);
                }
            }
            
            var dishIds = selectedDishes.Select(d => d.Id).ToArray();
           
            
            var customerId = Guid.NewGuid().ToString();
                
            var order = new Order
            {
                CustomerId = customerId,
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
                var message = Encoding.ASCII.GetString(data, 0, recv);
                Application.Current.Dispatcher.Invoke(new Action(() => { Message.Content = $"Klar att hämta: {message}"; }));
                
               
            }
        }

        private void BtnChose_OnClick(object sender, RoutedEventArgs e)
        {
            var dish = ListViewDishes.SelectedItem;
            ListViewOrders.Items.Add(dish);
        }

        private void BtnOrder_OnClick(object sender, RoutedEventArgs e)
        {
            ClientSend();
            ListViewOrders.Items.Clear();
            Message.Content = "Beställning Skickad";
        }
    }
}
    

