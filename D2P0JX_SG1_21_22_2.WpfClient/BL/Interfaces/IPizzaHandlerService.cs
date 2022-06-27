using D2P0JX_SG1_21_22_2.WpfClient.Models;
using System.Collections.Generic;

namespace D2P0JX_SG1_21_22_2.WpfClient.BL.Interfaces
{
    public interface IPizzaHandlerService
    {
        void AddPizza(IList<PizzaModel> collection);
        void ModifyPizza(IList<PizzaModel> collection, PizzaModel pizza);
        void DeletePizza(IList<PizzaModel> collection, PizzaModel pizza);
        void ViewPizza(PizzaModel pizza);

        IList<PizzaModel> GetAll();
        IList<RestaurantModel> GetAllRestaurants();
        IList<CustomerModel> GetAllCustomers();
    }
}
