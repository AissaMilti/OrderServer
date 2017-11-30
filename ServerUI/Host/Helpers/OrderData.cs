using Host.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Host.Helpers
{
    public static class OrderData
    {
        public static List<Order> Orders = new List<Order>();     
        public static ObservableCollection<Order> OrdersObsverableCollection = new ObservableCollection<Order>();
      
      
        
    }


    
}
