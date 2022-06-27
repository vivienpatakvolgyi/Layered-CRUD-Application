using D2P0JX_SG1_21_22_2.Logic.Interfaces;
using D2P0JX_SG1_21_22_2.Logic.Models;
using D2P0JX_SG1_21_22_2.Models.Entities;
using D2P0JX_SG1_21_22_2.Models.Models;
using D2P0JX_SG1_21_22_2.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace D2P0JX_SG1_21_22_2.Logic.Services
{
    public class RestaurantLogic : IRestaurantLogic
    {
        IPizzaRepository _PizzaRepository;
        IRestaurantRepository _RestaurantRepository;
        ICustomerRepository _CustomerRepository;

        public RestaurantLogic(IPizzaRepository pizzaRepository, IRestaurantRepository restaurantRepository, ICustomerRepository customerRepository)
        {
            _PizzaRepository = pizzaRepository;
            _RestaurantRepository = restaurantRepository;
            _CustomerRepository = customerRepository;
        }

        public IList<Restaurant> ReadAll()
        {
            return _RestaurantRepository.ReadAll().ToList();
        }

        public Restaurant Read(int id)
        {
            return _RestaurantRepository.Read(id);
        }

        public Restaurant Create(Restaurant entity)
        {
            if (_RestaurantRepository.Read(entity.Id) is null)
            {
                var result = _RestaurantRepository.Create(entity);
                return result;

            }
            else throw new ArgumentException("This restaurant already exists in the database!");
        }

        public Restaurant Update(Restaurant entity)
        {
            try
            {
                var result = _RestaurantRepository.Update(entity);
                return result;
            }
            catch (Exception)
            {
                throw new ArgumentException("Failed to update data. This restaurant is not found in the database");
            }
        }
        public void Delete(int id)
        {
            var entity = _RestaurantRepository.Read(id);
            if (entity == null ? false : entity.Pizzas.Any())
            {
                throw new Exception("Failed to delete this Restaurant, there are pizza items associated with this item.");
            }
            _RestaurantRepository.Delete(id);
        }

        public IEnumerable<AverageModel> GetRestaurantAverages()
        {
            var averages = from pizza in _PizzaRepository.ReadAll().ToList()
                           group pizza by pizza.RestaurantId into grouped
                           select new
                           {
                               RestaurantId = grouped.Key,
                               Average = grouped != null ? grouped.Average(x => x.Price) : 0
                           };
            var result = from restaurant in _RestaurantRepository.ReadAll().ToList()
                         from average in averages.ToList().Where(x => x.RestaurantId == restaurant.Id).DefaultIfEmpty()
                         select new AverageModel()
                         {
                             RestaurantName = restaurant.Name,
                             Average = average != null ? average.Average : 0
                         };
            return result.ToList();
        }

        public IEnumerable<MostPopularRestaurantModel> GetMostPopularRestaurant()
        {

            //pizzánként az értékek
            var pizzaswithcustomercount = from pizza in _PizzaRepository.ReadAll().ToList()
                                          join restaurant in _RestaurantRepository.ReadAll().ToList()
                                          on pizza.RestaurantId equals restaurant.Id
                                          select new
                                          {
                                              Name = pizza.Name,
                                              RestaurantId = pizza.RestaurantId,
                                              RestaurantName = restaurant.Name,
                                              Customercount = pizza.Customers != null ? pizza.Customers.Count : 0
                                          };


            var customercount = from pizza in pizzaswithcustomercount.ToList()
                                group pizza by pizza.RestaurantId into grouped
                                select new
                                {
                                    RestaurantId = grouped.Key,
                                };

            var list = new List<Temp>();

            foreach (var item in customercount)
            {
                Temp s = new Temp();
                foreach (var item2 in pizzaswithcustomercount)
                {
                    if (item.RestaurantId == item2.RestaurantId)
                    {
                        s.name = item2.RestaurantName;
                        s.count += item2.Customercount;

                    }

                }
                list.Add(s);
            }


            var result1 = (from top in list
                           orderby top.count descending
                           select new
                           {
                               RestaurantName = top != null ? top.name : ""
                           }).Take(1);
            var result = from r in result1
                         select new MostPopularRestaurantModel()
                         {
                             RestaurantName = r.RestaurantName
                         };
            return result.ToList();
        }

        public IEnumerable<IncomeModel> GetIncomes()
        {


            var customerspaymentperpizza = from pizza in _PizzaRepository.ReadAll().ToList()
                                           join restaurant in _RestaurantRepository.ReadAll().ToList()
                                           on pizza.RestaurantId equals restaurant.Id
                                           select new
                                           {
                                               Name = pizza.Name,
                                               RestaurantName = restaurant.Name,
                                               Income = pizza.Customers != null ? (pizza.Customers.Count * pizza.Price) : 0
                                           };


            var restaurantcount = from pizza in customerspaymentperpizza.ToList()
                                  group pizza by pizza.RestaurantName into grouped
                                  select new
                                  {
                                      RestaurantName = grouped.Key,
                                  };

            var list = new List<Temp2>();

            foreach (var item in restaurantcount)
            {
                Temp2 s = new Temp2();
                s.RestaurantName = item.RestaurantName;
                foreach (var item2 in customerspaymentperpizza)
                {
                    if (item.RestaurantName == item2.RestaurantName)
                    {
                        s.Income += item2.Income;


                    }

                }
                list.Add(s);

            }


            var result = from income in list
                         select new IncomeModel()
                         {
                             RestaurantName = income.RestaurantName,
                             Income = income != null ? income.Income : 0
                         };
            return result.ToList();
        }
        public class Temp
        {
            public int count { get; set; }
            public string name { get; set; }
        }
        public class Temp2
        {
            public int Income { get; set; }
            public string RestaurantName { get; set; }
        }

    }
}
