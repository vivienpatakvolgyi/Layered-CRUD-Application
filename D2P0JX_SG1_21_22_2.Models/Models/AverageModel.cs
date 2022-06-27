namespace D2P0JX_SG1_21_22_2.Logic.Models
{
    public class AverageModel
    {
        public string RestaurantName { get; set; }
        public double Average { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as AverageModel;

            if (other == null)
                return false;

            return other.RestaurantName == RestaurantName && other.Average == Average;
        }

        public override string ToString()
        {
            return $"{RestaurantName} - {Average}";
        }
    }
}
