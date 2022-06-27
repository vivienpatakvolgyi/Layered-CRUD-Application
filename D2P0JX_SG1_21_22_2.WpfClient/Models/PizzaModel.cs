using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace D2P0JX_SG1_21_22_2.WpfClient.Models
{
    public class PizzaModel : ObservableObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { Set(ref id, value); }
        }

        private int price;

        public int Price
        {
            get { return price; }
            set { Set(ref price, value); }
        }

        private int size;

        public int Size
        {
            get { return size; }
            set { Set(ref size, value); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }
        private int restaurantId;

        public int RestaurantId
        {
            get { return restaurantId; }
            set { Set(ref restaurantId, value); }
        }

        public string RestaurantName
        {
            get { return GetRestaurantName(); }
        }
        public string GetRestaurantName()
        {
            var restaurants = new List<RestaurantModel>()
            {
                new RestaurantModel(1,"DonPepe"),
                new RestaurantModel(2,"PizzaHut"),
                new RestaurantModel(3,"PizzaMe")
            };
            foreach (var item in restaurants)
            {
                if (RestaurantId == item.Id)
                {
                    return item.Name;
                }
            }
            return "-";
        }
        private bool isGlutenFree;
        public bool IsGlutenFree
        {
            get { return isGlutenFree; }
            set { Set(ref isGlutenFree, value); }
        }
        public string GlutenFree
        {
            get { return GetGlutenFree(); }
        }
        public string GetGlutenFree()
        {
            if (IsGlutenFree ==true)
            {
                return "P";
            }
            else
            {
                return "O";
            }
        }

        public PizzaModel()
        {

        }
        public PizzaModel(int id, int price, int size, string name, int restaurantID, bool isGlutenFree)
        {
            this.id = id;
            this.price = price;
            this.size = size;
            this.name = name;
            this.restaurantId = restaurantID;
            this.isGlutenFree = isGlutenFree;
        }
        public PizzaModel(PizzaModel other)
        {
            id = other.Id;
            price = other.Price;
            size = other.Size;
            name = other.Name;
            restaurantId = other.RestaurantId;
            isGlutenFree = other.IsGlutenFree;
        }


    }
}
