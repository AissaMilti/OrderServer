using OrderServer.Host.Models;

namespace OrderServer.Host.Data
{
    public class DataIntializer
    {
        public static void Seed()
        {
            var dish1 = new Dish()
            {
                Id = 1,
                Name = "Hamburgare",
                Price = 80
            };

            var dish2 = new Dish()
            {
                Id = 2,
                Name = "Halal Hamburgare",
                Price = 120
            };

            Context.Dishes.Add(dish1);
            Context.Dishes.Add(dish2);
        }
    }
}
