using D2P0JX_SG1_21_22_2.Logic.Interfaces;
using D2P0JX_SG1_21_22_2.Models.Entities;
using D2P0JX_SG1_21_22_2.Models.Models;
using D2P0JX_SG1_21_22_2.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace D2P0JX_SG1_21_22_2.Logic.Services
{
    public class PizzaLogic : IPizzaLogic
    {
        IPizzaRepository _PizzaRepository;
        IRestaurantRepository _RestaurantRepository;
        ICustomerRepository _CustomerRepository;

        public PizzaLogic(IPizzaRepository pizzaRepository, IRestaurantRepository restaurantRepository, ICustomerRepository customerRepository)
        {
            _PizzaRepository = pizzaRepository;
            _RestaurantRepository = restaurantRepository;
            _CustomerRepository = customerRepository;
        }

        public Pizza Create(Pizza entity)
        {
            if (_PizzaRepository.Read(entity.Id) is null)
            {
                var result = _PizzaRepository.Create(entity);
                return result;

            }
            else throw new ArgumentException("This pizza already exists in the database!");
        }

        public Pizza Read(int id)
        {
            return _PizzaRepository.Read(id);
        }

        public IList<Pizza> ReadAll()
        {
            return _PizzaRepository.ReadAll().ToList();
        }

        public Pizza Update(Pizza entity)
        {
            try
            {
                var result = _PizzaRepository.Update(entity);
                return result;
            }
            catch (Exception)
            {
                throw new Exception("Failed to update data. This pizza is not found in the database");
            }
        }
        public void Delete(int id)
        {

            _PizzaRepository.Delete(id);
        }



        public IEnumerable<MostPopularPizzaModel> GetMostPopularPizzas()
        {


            var pizzaswithcustomercount = from pizza in _PizzaRepository.ReadAll().ToList()
                                          select new
                                          {
                                              PopularPizzaName = pizza.Name,
                                              RestaurantId = pizza.RestaurantId,
                                              Count = pizza.Customers != null ? pizza.Customers.Count : 0
                                          };


            var restaurants = from r in pizzaswithcustomercount.ToList()
                              group r.PopularPizzaName by r.RestaurantId into g
                              select new
                              {
                                  Name = g.Key,
                              };

            var result2 = from pizza in pizzaswithcustomercount.ToList()
                          from r in _RestaurantRepository.ReadAll().ToList().Where(x => x.Id == pizza.RestaurantId).DefaultIfEmpty()
                          orderby r.Name descending
                          orderby pizza.Count descending
                          select new MostPopularPizzaModel()
                          {
                              MostPopularPizza = pizza != null ? pizza.PopularPizzaName : "",
                              RestaurantName = pizza != null ? r.Name : ""
                          };

            var result = result2.Take(restaurants.Count());



            return result;
        }

        public IEnumerable<BestOfferModel> GetBestOffers()
        {

            //pizzánként az értékek
            var worthswithname = from pizza in _PizzaRepository.ReadAll()
                                 select new
                                 {
                                     Name = pizza.Name,
                                     RestaurantId = pizza.RestaurantId,
                                     Worth = pizza != null ? (double)pizza.Price / Math.Sqrt((double)pizza.Size) * Math.PI : (double)0
                                 };
            //éttermenként a legjobb pizza ajánlat értéke
            var bestoffers = from pizza in worthswithname
                             group pizza by pizza.RestaurantId into grouped
                             select new
                             {
                                 RestaurantName = grouped.Key,
                                 Best = grouped.Min(x => x.Worth)
                             };

            var result = from best in bestoffers.ToList()
                         from offer in worthswithname.ToList().Where(x => x.Worth == best.Best).DefaultIfEmpty()
                         join name in _RestaurantRepository.ReadAll().DefaultIfEmpty()
                         on offer.RestaurantId equals name.Id
                         //on offer.Worth  equals best.Best
                         //where best.Best == offer.Worth
                         select new BestOfferModel()
                         {
                             RestaurantName = name.Name,
                             BestPizza = offer != null ? offer.Name : ""
                         };

            return result.ToList();
        }



        public List<Pizza> ReadAllByRestaurantId(int restaurantId)
        {
            return _PizzaRepository.ReadAll().Where(x => x.RestaurantId == restaurantId).ToList();
        }
    }

}
