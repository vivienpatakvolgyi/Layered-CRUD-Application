namespace D2P0JX_SG1_21_22_2.Models.Models
{
    public class MostPopularRestaurantModel
    {
        public string RestaurantName { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as BestOfferModel;

            if (other == null)
                return false;

            return other.RestaurantName == RestaurantName;
        }

        public override string ToString()
        {
            return $"{RestaurantName} is the most popular Restaurant.";
        }
    }
}
