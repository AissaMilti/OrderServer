namespace Host.Models
{
    public class Order
    {
        public string CustomerId { get; set; }
        public int[] DishIdArray { get; set; }
        public bool Done { get; set; }
    }
}
