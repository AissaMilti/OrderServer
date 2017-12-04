using Host.Models;

namespace Host.Data
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
                Name = "Veg Hamburgare",
                Price = 70
            };
            var dish3 = new Dish()
            {
                Id = 3,
                Name = "Fisk Hamburgare",
                Price = 80
            };
            var dish4 = new Dish()
            {
                Id = 4,
                Name = "Cheese Hamburgare",
                Price = 90
            };
            var dish5 = new Dish()
            {
                Id =52,
                Name = "Cola",
                Price = 15
            };
            var dish6 = new Dish()
            {
                Id = 6,
                Name = "Fanta",
                Price = 15
            };
            var dish7 = new Dish()
            {
                Id = 7,
                Name = "Chili cheese",
                Price = 45
            };
            var dish8 = new Dish()
            {
                Id = 8,
                Name = "Sallad",
                Price = 45
            };

            Context.Dishes.Add(dish1);
            Context.Dishes.Add(dish2);
            Context.Dishes.Add(dish3);
            Context.Dishes.Add(dish4);
            Context.Dishes.Add(dish5);
            Context.Dishes.Add(dish6);
            Context.Dishes.Add(dish7);
            Context.Dishes.Add(dish8);
        }
    }
}
