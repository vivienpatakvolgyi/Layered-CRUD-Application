using D2P0JX_SG1_21_22_2.Logic.Interfaces;
using D2P0JX_SG1_21_22_2.Models.Entities;
using D2P0JX_SG1_21_22_2.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace D2P0JX_SG1_21_22_2.Logic.Services
{
    public class CustomerLogic : ICustomerLogic
    {
        IPizzaRepository _PizzaRepository;
        IRestaurantRepository _RestaurantRepository;
        ICustomerRepository _CustomerRepository;

        public CustomerLogic(IPizzaRepository pizzaRepository, IRestaurantRepository restaurantRepository, ICustomerRepository customerRepository)
        {
            _PizzaRepository = pizzaRepository;
            _RestaurantRepository = restaurantRepository;
            _CustomerRepository = customerRepository;
        }

        public Customer Create(Customer entity)
        {

            if (_CustomerRepository.Read(entity.Id) is null)
            {
                var result = _CustomerRepository.Create(entity);
                return result;

            }
            else throw new ArgumentException("This customer already exists in the database!");


        }

        public Customer Read(int id)
        {
            return _CustomerRepository.Read(id);
        }

        public IList<Customer> ReadAll()
        {
            return _CustomerRepository.ReadAll().ToList();
        }

        public Customer Update(Customer entity)
        {
            try
            {
                var result = _CustomerRepository.Update(entity);
                return result;
            }
            catch (Exception)
            {
                throw new ArgumentException("Failed to update data. This user is not found in the database");
            }

        }

        public void Delete(int id)
        {

            _CustomerRepository.Delete(id);
        }


        public List<Customer> ReadAllByPizzaId(int pizzaId)
        {
            return _CustomerRepository.ReadAll().Where(x => x.PizzaId == pizzaId).ToList();
        }
    }

}

