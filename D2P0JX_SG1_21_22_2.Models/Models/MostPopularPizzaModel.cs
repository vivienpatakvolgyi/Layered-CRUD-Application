namespace D2P0JX_SG1_21_22_2.Models.Models
{
    public class MostPopularPizzaModel
    {
        public string RestaurantName { get; set; }
        public string MostPopularPizza { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as MostPopularPizzaModel;

            if (other == null)
                return false;

            return other.RestaurantName == RestaurantName && other.MostPopularPizza == MostPopularPizza;
        }

        public override string ToString()
        {
            return $"At {RestaurantName}, {MostPopularPizza} is the most popular pizza.";
        }
    }
}

