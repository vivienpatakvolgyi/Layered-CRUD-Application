
using D2P0JX_SG1_21_22_2.Client.Infrastructure;
using D2P0JX_SG1_21_22_2.Logic.Models;
using D2P0JX_SG1_21_22_2.Models.Entities;
using D2P0JX_SG1_21_22_2.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace D2P0JX_SG1_21_22_2.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for server..");
            Console.ReadLine();
            //OldTesting();
            var httpService = new HttpService("Pizza", "http://localhost:30944/api/");
            var httpService2 = new HttpService("Restaurant", "http://localhost:30944/api/");
            var httpService3 = new HttpService("Customer", "http://localhost:30944/api/");
            // Get All
            var pizzas = httpService.GetAll<Pizza>();
            DisplayPizzas(pizzas);

            var restaurants = httpService2.GetAll<Restaurant>();
            DisplayRestaurants(restaurants);

            var customers = httpService3.GetAll<Customer>();
            DisplayCustomers(customers);
            Console.WriteLine();
            // Get one
            var onePizza = httpService.Get<Pizza, int>(pizzas.First().Id);
            DisplayPizza(onePizza);
            var oneRestaurant = httpService2.Get<Restaurant, int>(restaurants.First().Id);
            DisplayRestaurant(oneRestaurant);
            var oneCustomer = httpService3.Get<Customer, int>(customers.First().Id);
            DisplayCustomer(oneCustomer);

            // Create
            var newPizza = new Pizza()
            {
                Name = "Four seasons Pizza",
                RestaurantId = 1,
                Price = 5200,
                Size = 30,
                IsGlutenFree = true
            };

            var newRestaurant = new Restaurant()
            {
                Name = "Pizza Me",

            };

            var newCustomer = new Customer()
            {
                Name = "Farkas Jolán",
                PizzaId = 1
            };

            var result = httpService.Create(newPizza);
            var result2 = httpService2.Create(newRestaurant);
            var result3 = httpService3.Create(newCustomer);

            if (result.IsSuccess)
            {
                Console.WriteLine("Pizza creation was succesfull");
            }
            if (result2.IsSuccess)
            {
                Console.WriteLine("Restaurant creation was succesfull");
            }
            if (result3.IsSuccess)
            {
                Console.WriteLine("Customer creation was succesfull");
            }

            // Check
            pizzas = httpService.GetAll<Pizza>();
            DisplayPizzas(pizzas);

            restaurants = httpService2.GetAll<Restaurant>();
            DisplayRestaurants(restaurants);

            customers = httpService3.GetAll<Customer>();
            DisplayCustomers(customers);

            // Update
            var pizzaForUpdate = pizzas.First();
            pizzaForUpdate.Name = "Chili Pizza";
            pizzaForUpdate.Price = 123568;

            result = httpService.Update(pizzaForUpdate);

            if (result.IsSuccess)
            {
                Console.WriteLine("Pizza update was successfull.");
            }

            var restaurantForUpdate = restaurants.First();
            restaurantForUpdate.Name = "Avanti";

            result2 = httpService2.Update(restaurantForUpdate);

            if (result2.IsSuccess)
            {
                Console.WriteLine("Restaurant update was successfull.");
            }

            var customerForUpdate = customers.First();
            customerForUpdate.Name = "Pénteki László";
            customerForUpdate.PizzaId = 2;

            result3 = httpService3.Update(customerForUpdate);

            if (result3.IsSuccess)
            {
                Console.WriteLine("Customer update was successfull.");
            }

            // Check
            httpService = new HttpService("Pizza", "http://localhost:30944/api/");
            pizzas = httpService.GetAll<Pizza>();
            DisplayPizzas(pizzas);

            httpService2 = new HttpService("Restaurant", "http://localhost:30944/api/");
            restaurants = httpService2.GetAll<Restaurant>();
            DisplayRestaurants(restaurants);

            httpService3 = new HttpService("Customer", "http://localhost:30944/api/");
            customers = httpService3.GetAll<Customer>();
            DisplayCustomers(customers);

            // Delete
            result = httpService.Delete(pizzaForUpdate.Id);

            if (result.IsSuccess)
            {
                Console.WriteLine("Pizza deletion was successfull.");
            }
            else
            {
                Console.WriteLine("Pizza deletion was unsuccessfull.");
            }

            result2 = httpService2.Delete(restaurantForUpdate.Id);

            if (result2.IsSuccess)
            {
                Console.WriteLine("Restaurant deletion was successfull.");
            }
            else
            {
                Console.WriteLine("Restaurant deletion was unsuccessfull.");
            }

            result3 = httpService3.Delete(customerForUpdate.Id);

            if (result3.IsSuccess)
            {
                Console.WriteLine("Customer deletion was successfull.");
            }
            else
            {
                Console.WriteLine("Customer deletion was unsuccessfull.");
            }

            // Check
            httpService = new HttpService("Pizza", "http://localhost:30944/api/");
            pizzas = httpService.GetAll<Pizza>();
            DisplayPizzas(pizzas);

            httpService2 = new HttpService("Restaurant", "http://localhost:30944/api/");
            restaurants = httpService2.GetAll<Restaurant>();
            DisplayRestaurants(restaurants);

            httpService3 = new HttpService("Customer", "http://localhost:30944/api/");
            customers = httpService3.GetAll<Customer>();
            DisplayCustomers(customers);

            // NON-CRUD METHODS
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("AVERAGES:");
            var restaurantAverages = httpService2.GetAll<AverageModel>("GetRestaurantAverages");
            DisplayRestaurantAverages(restaurantAverages);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("BEST OFFERS:");
            var bestoffers = httpService.GetAll<BestOfferModel>("GetBestOffers");
            DisplayBestOffers(bestoffers);
            Console.WriteLine();
            var mostpopularrestaurant = httpService2.GetAll<MostPopularRestaurantModel>("GetMostPopularRestaurant");
            DisplayMostPopularRestaurant(mostpopularrestaurant);
            var mostpopularpizzas = httpService.GetAll<MostPopularPizzaModel>("GetMostPopularPizzas");
            DisplayMostPopularPizza(mostpopularpizzas);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("TOTAL INCOMES:");
            var income = httpService2.GetAll<IncomeModel>("GetIncomes");
            DisplayIncome(income);
            Console.ReadLine();
        }


        //NON-CRUD DISPLAY

        private static void DisplayIncome(List<IncomeModel> income)
        {
            Console.WriteLine();

            foreach (var i in income)
            {
                Console.WriteLine(i);
            }
        }

        private static void DisplayMostPopularPizza(List<MostPopularPizzaModel> mostpopularpizzas)
        {
            Console.WriteLine();

            foreach (var popular in mostpopularpizzas)
            {
                Console.WriteLine(popular);
            }
        }
        private static void DisplayMostPopularRestaurant(List<MostPopularRestaurantModel> mostpopularrestaurant)
        {
            Console.WriteLine();

            foreach (var popular in mostpopularrestaurant)
            {
                Console.WriteLine(popular);
            }
        }
        private static void DisplayBestOffers(List<BestOfferModel> bestOffers)
        {
            Console.WriteLine();

            foreach (var best in bestOffers)
            {
                Console.WriteLine(best);
            }
        }

        private static void DisplayRestaurantAverages(List<AverageModel> restaurantAverages)
        {
            Console.WriteLine();

            foreach (var restaurantAverage in restaurantAverages)
            {
                Console.WriteLine(restaurantAverage);
            }
        }
        //DISPLAY
        private static void DisplayPizza(Pizza pizza)
        {
            Console.WriteLine("Id:{0}\nName:{1}\nPrice:{2}\nRestaurantId:{3}", pizza.Id, pizza.Name, pizza.Price, pizza.RestaurantId, pizza.IsGlutenFree);
        }
        private static void DisplayRestaurant(Restaurant restaurant)
        {
            var pizzanames = "";
            foreach (var item in restaurant.Pizzas)
            {
                pizzanames += item.Name.ToString() + "  ";
            }
            Console.WriteLine("Id:{0}\nName:{1}\nPizzas:{2}", restaurant.Id, restaurant.Name, pizzanames);
        }
        private static void DisplayCustomer(Customer customer)
        {
            Console.WriteLine("Name:{0}\nPizza ID:{1}\nCustomer ID: {2}", customer.Name, customer.PizzaId, customer.Id);
        }

        //MOAR DISPLAY
        private static void DisplayPizzas(List<Pizza> pizzas)
        {
            Console.WriteLine();

            foreach (var pizza in pizzas)
            {
                DisplayPizza(pizza);
            }
        }
        private static void DisplayRestaurants(List<Restaurant> restaurants)
        {
            Console.WriteLine();

            foreach (var restaurant in restaurants)
            {
                DisplayRestaurant(restaurant);
            }
        }
        private static void DisplayCustomers(List<Customer> customers)
        {
            Console.WriteLine();

            foreach (var customer in customers)
            {
                DisplayCustomer(customer);
            }
        }

    }
}

