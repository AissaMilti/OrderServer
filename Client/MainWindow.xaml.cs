﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;
using Client.Models;
using System.Collections.ObjectModel;

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
            //Console.WriteLine("Avsluta med 'exit'");
            while (true)
            {
                //var message = Console.ReadLine();
                //var bytesToSend = Encoding.ASCII.GetBytes(message);
                //ns.Write(bytesToSend, 0, bytesToSend.Length);
                //ns.Flush();
                //if (message == "exit") break;
            }
        }
        private void ClientRecieve()
        {
            //Console.WriteLine("Tar emot data från servern....");

            while (true)
            {
                var data = new byte[1024];
                var recv = ns.Read(data, 0, data.Length);
                var message = Encoding.ASCII.GetString(data, 0, recv);
               
             
                //Console.WriteLine("\t\t<" + message + ">");
            }
        }
    }
}
    

