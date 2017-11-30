namespace Client.Models
{
    public class Order
    {
        public int? CustomerId { get; set; }
        public int[] DishIdArray { get; set; }
        public bool Done { get; set; }
    }
}
