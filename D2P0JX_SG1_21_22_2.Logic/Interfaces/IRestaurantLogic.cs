using D2P0JX_SG1_21_22_2.Logic.Models;
using D2P0JX_SG1_21_22_2.Models.Entities;
using D2P0JX_SG1_21_22_2.Models.Models;
using System.Collections.Generic;

namespace D2P0JX_SG1_21_22_2.Logic.Interfaces
{
    public interface IRestaurantLogic
    {
        IList<Restaurant> ReadAll();
        Restaurant Read(int id);
        Restaurant Create(Restaurant entity);
        Restaurant Update(Restaurant entity);
        void Delete(int id);
        IEnumerable<AverageModel> GetRestaurantAverages();
        IEnumerable<MostPopularRestaurantModel> GetMostPopularRestaurant();
        IEnumerable<IncomeModel> GetIncomes();
    }
}
