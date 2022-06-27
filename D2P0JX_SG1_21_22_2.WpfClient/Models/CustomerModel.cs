using GalaSoft.MvvmLight;

namespace D2P0JX_SG1_21_22_2.WpfClient.Models
{
    public class CustomerModel : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PizzaId { get; set; }
        public CustomerModel()
        {

        }

        public CustomerModel(int id, string name, int pizzaId)
        {
            Id = id;
            Name = name;
            PizzaId = pizzaId;
        }
    }
}
