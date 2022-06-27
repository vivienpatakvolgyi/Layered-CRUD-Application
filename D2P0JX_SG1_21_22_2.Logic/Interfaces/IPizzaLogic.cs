using D2P0JX_SG1_21_22_2.Models.Entities;
using D2P0JX_SG1_21_22_2.Models.Models;
using System.Collections.Generic;

namespace D2P0JX_SG1_21_22_2.Logic.Interfaces
{
    public interface IPizzaLogic
    {
        IList<Pizza> ReadAll();
        Pizza Read(int id);
        Pizza Create(Pizza entity);
        Pizza Update(Pizza entity);
        void Delete(int id);
        List<Pizza> ReadAllByRestaurantId(int pizzaId);
        IEnumerable<BestOfferModel> GetBestOffers();
        IEnumerable<MostPopularPizzaModel> GetMostPopularPizzas();
    }
}
