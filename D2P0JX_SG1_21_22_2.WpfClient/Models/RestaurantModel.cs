using GalaSoft.MvvmLight;

namespace D2P0JX_SG1_21_22_2.WpfClient.Models
{
    public class RestaurantModel : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RestaurantModel()
        {

        }

        public RestaurantModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
