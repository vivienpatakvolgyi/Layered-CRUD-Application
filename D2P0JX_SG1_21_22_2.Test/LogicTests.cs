using D2P0JX_SG1_21_22_2.Logic.Models;
using D2P0JX_SG1_21_22_2.Logic.Services;
using D2P0JX_SG1_21_22_2.Models.Entities;
using D2P0JX_SG1_21_22_2.Models.Models;
using D2P0JX_SG1_21_22_2.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace D2P0JX_HFT_2021221.Test
{
    [TestFixture]
    class LogicTests
    {
        #region Constants

        const int Restaurant1Id = 1;
        const int Restaurant2Id = 2;
        const int Pizza1Id = 1;
        const int Pizza2Id = 2;
        const string Restaurant1Name = "r1Test";
        const string Restaurant2Name = "r2Test";



        #endregion

        [TestCase]
        public void RestaurantCreationTest()
        {
            //Arrange
            var pizzaRepo = new Mock<IPizzaRepository>();
            var restaurantRepo = new Mock<IRestaurantRepository>();
            var customerRepo = new Mock<ICustomerRepository>();

            Restaurant restaurant = new Restaurant() { Id = 1, Name = "Olivio" };

            restaurantRepo.Setup(x => x.Create(It.IsAny<Restaurant>())).Returns(restaurant);

            var logic = new RestaurantLogic(pizzaRepo.Object, restaurantRepo.Object, customerRepo.Object);

            //Act
            var result = logic.Create(restaurant);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(restaurant));
        }
        [TestCase]
        public void PizzaCreationTest()
        {
            //Arrange
            var pizzaRepo = new Mock<IPizzaRepository>();
            var restaurantRepo = new Mock<IRestaurantRepository>();
            var customerRepo = new Mock<ICustomerRepository>();

            Pizza pizza = new Pizza() { Id = 1, Name = "Chili Pizza", RestaurantId = 1, Size = 25 };

            pizzaRepo.Setup(x => x.Create(It.IsAny<Pizza>())).Returns(pizza);

            var logic = new PizzaLogic(pizzaRepo.Object, restaurantRepo.Object, customerRepo.Object);

            //Act
            var result = logic.Create(pizza);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(pizza));
        }
        [TestCase]
        public void CustomerCreationTest()
        {
            //Arrange
            var pizzaRepo = new Mock<IPizzaRepository>();
            var restaurantRepo = new Mock<IRestaurantRepository>();
            var customerRepo = new Mock<ICustomerRepository>();

            Customer customer = new Customer() { Id = 1, Name = "Mézga Géza", PizzaId = 1 };

            customerRepo.Setup(x => x.Create(It.IsAny<Customer>())).Returns(customer);

            var logic = new CustomerLogic(pizzaRepo.Object, restaurantRepo.Object, customerRepo.Object);

            //Act
            var result = logic.Create(customer);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(customer));
        }
        [TestCase]
        public void DeletePizzaTest()
        {
            //Arrange
            var pizzaRepo = new Mock<IPizzaRepository>();
            var restaurantRepo = new Mock<IRestaurantRepository>();
            var customerRepo = new Mock<ICustomerRepository>();

            Pizza pizza = new Pizza() { Id = 1, Name = "Chili Pizza", RestaurantId = 1, Size = 25 };

            pizzaRepo.Setup(x => x.Delete(It.IsAny<int>()));

            var logic = new PizzaLogic(pizzaRepo.Object, restaurantRepo.Object, customerRepo.Object);

            //Act
            logic.Delete(pizza.Id);

            //Assert
            pizzaRepo.Verify(x => x.Delete(pizza.Id));


        }
        [TestCase]
        public void DeleteRestaurantTest()
        {
            //Arrange
            var pizzaRepo = new Mock<IPizzaRepository>();
            var restaurantRepo = new Mock<IRestaurantRepository>();
            var customerRepo = new Mock<ICustomerRepository>();

            Restaurant restaurant = new Restaurant() { Id = 1, Name = "Olivio" };

            restaurantRepo.Setup(x => x.Delete(It.IsAny<int>()));

            var logic = new PizzaLogic(pizzaRepo.Object, restaurantRepo.Object, customerRepo.Object);

            //Act
            logic.Delete(restaurant.Id);

            //Assert
            pizzaRepo.Verify(x => x.Delete(restaurant.Id));

        }
        [TestCase]
        public void UpdatePizzaTest()
        {
            //Arrange
            var pizzaRepo = new Mock<IPizzaRepository>();
            var restaurantRepo = new Mock<IRestaurantRepository>();
            var customerRepo = new Mock<ICustomerRepository>();

            Pizza pizza = new Pizza() { Id = 1, Name = "Chili Pizza", RestaurantId = 1, Size = 25 };

            pizzaRepo.Setup(x => x.Update(It.IsAny<Pizza>()));

            var logic = new PizzaLogic(pizzaRepo.Object, restaurantRepo.Object, customerRepo.Object);

            //Act
            logic.Update(pizza);

            //Assert
            pizzaRepo.Verify(x => x.Update(pizza));

        }
        [TestCase]
        public void UpdateCustomerTest()
        {
            //Arrange
            var pizzaRepo = new Mock<IPizzaRepository>();
            var restaurantRepo = new Mock<IRestaurantRepository>();
            var customerRepo = new Mock<ICustomerRepository>();

            Customer customer = new Customer() { Id = 1, Name = "Mézga Géza", PizzaId = 1 };

            customerRepo.Setup(x => x.Update(It.IsAny<Customer>()));

            var logic = new CustomerLogic(pizzaRepo.Object, restaurantRepo.Object, customerRepo.Object);

            //Act
            logic.Update(customer);

            //Assert
            customerRepo.Verify(x => x.Update(customer));

        }
        [TestCase]
        public void UpdateRestaurantTest()
        {
            //Arrange
            var pizzaRepo = new Mock<IPizzaRepository>();
            var restaurantRepo = new Mock<IRestaurantRepository>();
            var customerRepo = new Mock<ICustomerRepository>();

            Restaurant restaurant = new Restaurant() { Id = 1, Name = "Olivio" };

            customerRepo.Setup(x => x.Update(It.IsAny<Customer>()));

            var logic = new RestaurantLogic(pizzaRepo.Object, restaurantRepo.Object, customerRepo.Object);

            //Act
            logic.Update(restaurant);

            //Assert
            restaurantRepo.Verify(x => x.Update(restaurant));

        }

        [TestCaseSource(nameof(GetRestaurantAveragesData))]
        public void RestaurantAveragesTest(List<Restaurant> restaurants, List<Pizza> pizzas, List<Customer> customers, List<AverageModel> expectedAverages)
        {
            var pizzaRepo = new Mock<IPizzaRepository>();
            var restaurantRepo = new Mock<IRestaurantRepository>();
            var customerRepo = new Mock<ICustomerRepository>();

            pizzaRepo.Setup(x => x.ReadAll()).Returns(pizzas.AsQueryable());
            restaurantRepo.Setup(x => x.ReadAll()).Returns(restaurants.AsQueryable());
            customerRepo.Setup(x => x.ReadAll()).Returns(customers.AsQueryable());


            var logic = new RestaurantLogic(pizzaRepo.Object, restaurantRepo.Object, customerRepo.Object);

            // Act
            var result = logic.GetRestaurantAverages();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EquivalentTo(expectedAverages));
        }
        [TestCaseSource(nameof(GetBestOffersData))]
        public void GetBestOffersTest(List<Restaurant> restaurants, List<Pizza> pizzas, List<Customer> customers, List<BestOfferModel> expectedResult)
        {
            var pizzaRepo = new Mock<IPizzaRepository>();
            var restaurantRepo = new Mock<IRestaurantRepository>();
            var customerRepo = new Mock<ICustomerRepository>();

            pizzaRepo.Setup(x => x.ReadAll()).Returns(pizzas.AsQueryable());
            restaurantRepo.Setup(x => x.ReadAll()).Returns(restaurants.AsQueryable());
            customerRepo.Setup(x => x.ReadAll()).Returns(customers.AsQueryable());


            var logic = new PizzaLogic(pizzaRepo.Object, restaurantRepo.Object, customerRepo.Object);

            // Act
            var result = logic.GetBestOffers();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EquivalentTo(expectedResult));
        }
        [TestCaseSource(nameof(GetMostPopuarPizzasData))]
        public void GetMostPopularPizzasTest(List<Restaurant> restaurants, List<Pizza> pizzas, List<Customer> customers, List<MostPopularPizzaModel> expectedResult)
        {
            var pizzaRepo = new Mock<IPizzaRepository>();
            var restaurantRepo = new Mock<IRestaurantRepository>();
            var customerRepo = new Mock<ICustomerRepository>();

            pizzaRepo.Setup(x => x.ReadAll()).Returns(pizzas.AsQueryable());
            restaurantRepo.Setup(x => x.ReadAll()).Returns(restaurants.AsQueryable());
            customerRepo.Setup(x => x.ReadAll()).Returns(customers.AsQueryable());


            var logic = new PizzaLogic(pizzaRepo.Object, restaurantRepo.Object, customerRepo.Object);

            // Act
            var result = logic.GetMostPopularPizzas();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EquivalentTo(expectedResult));
        }

        [TestCaseSource(nameof(GetMostPopuarRestaurantData))]
        public void GetMostPopularRestaurantTest(List<Restaurant> restaurants, List<Pizza> pizzas, List<Customer> customers, List<MostPopularRestaurantModel> expectedResult)
        {
            var pizzaRepo = new Mock<IPizzaRepository>();
            var restaurantRepo = new Mock<IRestaurantRepository>();
            var customerRepo = new Mock<ICustomerRepository>();

            pizzaRepo.Setup(x => x.ReadAll()).Returns(pizzas.AsQueryable());
            restaurantRepo.Setup(x => x.ReadAll()).Returns(restaurants.AsQueryable());
            customerRepo.Setup(x => x.ReadAll()).Returns(customers.AsQueryable());


            var logic = new RestaurantLogic(pizzaRepo.Object, restaurantRepo.Object, customerRepo.Object);

            // Act
            var result = logic.GetMostPopularRestaurant().ToString();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedResult.ToString()));
        }
        [TestCaseSource(nameof(GetIncomesData))]
        public void GetIncomesTest(List<Restaurant> restaurants, List<Pizza> pizzas, List<Customer> customers, List<IncomeModel> expectedResult)
        {
            var pizzaRepo = new Mock<IPizzaRepository>();
            var restaurantRepo = new Mock<IRestaurantRepository>();
            var customerRepo = new Mock<ICustomerRepository>();

            pizzaRepo.Setup(x => x.ReadAll()).Returns(pizzas.AsQueryable());
            restaurantRepo.Setup(x => x.ReadAll()).Returns(restaurants.AsQueryable());
            customerRepo.Setup(x => x.ReadAll()).Returns(customers.AsQueryable());


            var logic = new RestaurantLogic(pizzaRepo.Object, restaurantRepo.Object, customerRepo.Object);

            // Act
            var result = logic.GetIncomes();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedResult));
        }



        #region utils


        public static List<TestCaseData> GetIncomesData()
        {
            var result = new List<TestCaseData>();

            // Empty elements
            result.Add(new TestCaseData(
                new List<Restaurant>(),
                new List<Pizza>(),
                new List<Customer>(),
                new List<IncomeModel>()
            ));

            // One restaurant withot pizza
            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name }
                },
                new List<Pizza>(),
                new List<Customer>(),
                new List<IncomeModel>()
                {

                }
            ));

            //One restaurant and one pizza without customer (bad id)
            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name }
                },
                new List<Pizza>()
                {
                    new Pizza() { Id = 1, RestaurantId = Restaurant1Id, Name = "TestPizz1", Price = 1200, Size = 32 }
                },
                new List<Customer>(),
                new List<IncomeModel>()
                {
                    new IncomeModel() { RestaurantName = Restaurant1Name, Income = 0 }
                }
            ));


            var customer1 = new Customer() { Id = 1, PizzaId = 1, Name = "TestCust1" };
            var customer2 = new Customer() { Id = 2, PizzaId = 1, Name = "TestCust2" };
            var customer3 = new Customer() { Id = 3, PizzaId = 1, Name = "TestCust3" };
            var customer4 = new Customer() { Id = 4, PizzaId = 4, Name = "TestCust4" };
            var customer5 = new Customer() { Id = 5, PizzaId = 4, Name = "TestCust5" };
            List<Customer> customers1 = new List<Customer>()
            {
                customer1, customer2, customer3
            };

            List<Customer> customers2 = new List<Customer>()
            {
                customer4, customer5
            };
            // Multiple restaurant with multiple pizza and customer
            var pizza1 = new Pizza() { Id = 1, RestaurantId = Restaurant1Id, Name = "TestPizz1", Price = 2500, Size = 32, Customers = customers1 };
            var pizza2 = new Pizza() { Id = 2, RestaurantId = Restaurant1Id, Name = "TestPizz2", Price = 1400, Size = 32 };
            var pizza3 = new Pizza() { Id = 3, RestaurantId = Restaurant2Id, Name = "TestPizz3", Price = 1000, Size = 32 };
            var pizza4 = new Pizza() { Id = 4, RestaurantId = Restaurant2Id, Name = "TestPizz4", Price = 2000, Size = 32, Customers = customers2 };
            var pizza5 = new Pizza() { Id = 5, RestaurantId = Restaurant2Id, Name = "TestPizz5", Price = 3001, Size = 32 };

            List<Pizza> pizzas1 = new List<Pizza>()
            {
                pizza1, pizza2
            };
            List<Pizza> pizzas2 = new List<Pizza>()
            {
                pizza3, pizza4, pizza5
            };
            result.Add(new TestCaseData(
                    new List<Restaurant>()
                    {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name, Pizzas = pizzas1 },
                    new Restaurant() { Id = Restaurant2Id, Name = Restaurant2Name, Pizzas = pizzas2},
                    },
                    new List<Pizza>()
                    {
                    pizza1, pizza2, pizza3, pizza4, pizza5
    },
                    new List<Customer>()
                    {
                    customer1, customer2, customer3, customer4, customer5
                    },
                    new List<IncomeModel>()
                    {
                    new IncomeModel() { RestaurantName = Restaurant1Name, Income = 7500 },
                    new IncomeModel() { RestaurantName = Restaurant2Name, Income = 4000 },

                    }
                ));
            return result;
        }
        public static List<TestCaseData> GetMostPopuarRestaurantData()
        {
            var result = new List<TestCaseData>();

            // Empty elements
            result.Add(new TestCaseData(
                new List<Restaurant>(),
                new List<Pizza>(),
                new List<Customer>(),
                new List<MostPopularRestaurantModel>()
            ));

            // One restaurant withot pizza
            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name }
                },
                new List<Pizza>(),
                new List<Customer>(),
                new List<MostPopularRestaurantModel>()
                {

                }
            ));

            //One restaurant and one pizza without customer (bad id)
            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name }
                },
                new List<Pizza>()
                {
                    new Pizza() { Id = 1, RestaurantId = Restaurant1Id, Name = "TestPizz1", Price = 1200, Size = 32 }
                },
                new List<Customer>(),
                new List<MostPopularRestaurantModel>()
                {
                    new MostPopularRestaurantModel() { RestaurantName = Restaurant1Name}
                }
            ));

            // Multiple restaurant with multiple pizza and customer

            var customer1 = new Customer() { Id = 1, PizzaId = 1, Name = "TestCust1" };
            var customer2 = new Customer() { Id = 2, PizzaId = 1, Name = "TestCust2" };
            var customer3 = new Customer() { Id = 3, PizzaId = 1, Name = "TestCust3" };
            var customer4 = new Customer() { Id = 4, PizzaId = 4, Name = "TestCust4" };
            var customer5 = new Customer() { Id = 5, PizzaId = 4, Name = "TestCust5" };
            List<Customer> customers1 = new List<Customer>()
            {
                customer1, customer2, customer3
            };

            List<Customer> customers2 = new List<Customer>()
            {
                customer4, customer5
            };

            var pizza1 = new Pizza() { Id = 1, RestaurantId = Restaurant1Id, Name = "TestPizz1", Price = 2500, Size = 32, Customers = customers1 };
            var pizza2 = new Pizza() { Id = 2, RestaurantId = Restaurant1Id, Name = "TestPizz2", Price = 1400, Size = 32 };
            var pizza3 = new Pizza() { Id = 3, RestaurantId = Restaurant2Id, Name = "TestPizz3", Price = 1000, Size = 32 };
            var pizza4 = new Pizza() { Id = 4, RestaurantId = Restaurant2Id, Name = "TestPizz4", Price = 2000, Size = 32, Customers = customers2 };
            var pizza5 = new Pizza() { Id = 5, RestaurantId = Restaurant2Id, Name = "TestPizz5", Price = 3001, Size = 32 };

            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name},
                    new Restaurant() { Id = Restaurant2Id, Name = Restaurant2Name},
                },
                new List<Pizza>()
                {
                    pizza1, pizza2, pizza3, pizza4, pizza5
                },
                new List<Customer>()
                {
                    customer1, customer2, customer3, customer4, customer5
                },
                new List<MostPopularRestaurantModel>()
                {
                    new MostPopularRestaurantModel() { RestaurantName = Restaurant1Name},


                }
            ));
            return result;
        }
        public static List<TestCaseData> GetMostPopuarPizzasData()
        {
            var result = new List<TestCaseData>();

            // Empty elements
            result.Add(new TestCaseData(
                new List<Restaurant>(),
                new List<Pizza>(),
                new List<Customer>(),
                new List<MostPopularPizzaModel>()
            ));

            // One restaurant withot pizza
            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name }
                },
                new List<Pizza>(),
                new List<Customer>(),
                new List<MostPopularPizzaModel>()
                {

                }
            ));

            //One restaurant and one pizza without customer (bad id)
            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name }
                },
                new List<Pizza>()
                {
                    new Pizza() { Id = 1, RestaurantId = Restaurant1Id, Name = "TestPizz1", Price = 1200, Size = 32 }
                },
                new List<Customer>(),
                new List<MostPopularPizzaModel>()
                {
                    new MostPopularPizzaModel() { RestaurantName = Restaurant1Name, MostPopularPizza = "TestPizz1" }
                }
            ));


            var customer1 = new Customer() { Id = 1, PizzaId = 1, Name = "TestCust1" };
            var customer2 = new Customer() { Id = 2, PizzaId = 1, Name = "TestCust2" };
            var customer3 = new Customer() { Id = 3, PizzaId = 1, Name = "TestCust3" };
            var customer4 = new Customer() { Id = 4, PizzaId = 4, Name = "TestCust4" };
            var customer5 = new Customer() { Id = 5, PizzaId = 4, Name = "TestCust5" };
            List<Customer> customers1 = new List<Customer>()
            {
                customer1, customer2, customer3
            };

            List<Customer> customers2 = new List<Customer>()
            {
                customer4, customer5
            };
            // Multiple restaurant with multiple pizza and customer
            var pizza1 = new Pizza() { Id = 1, RestaurantId = Restaurant1Id, Name = "TestPizz1", Price = 2500, Size = 32, Customers = customers1 };
            var pizza2 = new Pizza() { Id = 2, RestaurantId = Restaurant1Id, Name = "TestPizz2", Price = 1400, Size = 32 };
            var pizza3 = new Pizza() { Id = 3, RestaurantId = Restaurant2Id, Name = "TestPizz3", Price = 1000, Size = 32 };
            var pizza4 = new Pizza() { Id = 4, RestaurantId = Restaurant2Id, Name = "TestPizz4", Price = 2000, Size = 32, Customers = customers2 };
            var pizza5 = new Pizza() { Id = 5, RestaurantId = Restaurant2Id, Name = "TestPizz5", Price = 3001, Size = 32 };

            List<Pizza> pizzas1 = new List<Pizza>()
            {
                pizza1, pizza2
            };
            List<Pizza> pizzas2 = new List<Pizza>()
            {
                pizza3, pizza4, pizza5
            };
            result.Add(new TestCaseData(
                    new List<Restaurant>()
                    {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name, Pizzas = pizzas1 },
                    new Restaurant() { Id = Restaurant2Id, Name = Restaurant2Name, Pizzas = pizzas2},
                    },
                    new List<Pizza>()
                    {
                    pizza1, pizza2, pizza3, pizza4, pizza5
    },
                    new List<Customer>()
                    {
                    customer1, customer2, customer3, customer4, customer5
                    },
                    new List<MostPopularPizzaModel>()
                    {
                    new MostPopularPizzaModel() { RestaurantName = Restaurant1Name, MostPopularPizza = "TestPizz1" },
                    new MostPopularPizzaModel() { RestaurantName = Restaurant2Name, MostPopularPizza = "TestPizz4" },

                    }
                ));
            return result;

        }
        public static List<TestCaseData> GetBestOffersData()
        {
            var result = new List<TestCaseData>();

            // Empty elements
            result.Add(new TestCaseData(
                new List<Restaurant>(),
                new List<Pizza>(),
                new List<Customer>(),
                new List<BestOfferModel>()
            ));

            // One restaurant withot pizza
            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name }
                },
                new List<Pizza>(),
                new List<Customer>(),
                new List<BestOfferModel>()
                {

                }
            ));

            //One restaurant and one pizza without customer (bad id)
            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name }
                },
                new List<Pizza>()
                {
                    new Pizza() { Id = 1, RestaurantId = Restaurant1Id, Name = "TestPizz1", Price = 1200, Size = 32 }
                },
                new List<Customer>(),
                new List<BestOfferModel>()
                {
                    new BestOfferModel() { RestaurantName = Restaurant1Name, BestPizza = "TestPizz1" }
                }
            ));

            // Multiple restaurant with multiple pizza and customer
            var pizza1 = new Pizza() { Id = 1, RestaurantId = Restaurant1Id, Name = "TestPizz1", Price = 2500, Size = 32 };
            var pizza2 = new Pizza() { Id = 2, RestaurantId = Restaurant1Id, Name = "TestPizz2", Price = 1400, Size = 32 };
            var pizza3 = new Pizza() { Id = 3, RestaurantId = Restaurant2Id, Name = "TestPizz3", Price = 1000, Size = 32 };
            var pizza4 = new Pizza() { Id = 4, RestaurantId = Restaurant2Id, Name = "TestPizz4", Price = 2000, Size = 32 };
            var pizza5 = new Pizza() { Id = 5, RestaurantId = Restaurant2Id, Name = "TestPizz5", Price = 3001, Size = 32 };


            var customer1 = new Customer() { Id = 1, PizzaId = 1, Name = "TestCust1" };
            var customer2 = new Customer() { Id = 2, PizzaId = 1, Name = "TestCust2" };
            var customer3 = new Customer() { Id = 3, PizzaId = 1, Name = "TestCust3" };
            var customer4 = new Customer() { Id = 4, PizzaId = 2, Name = "TestCust4" };
            var customer5 = new Customer() { Id = 5, PizzaId = 2, Name = "TestCust5" };

            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name },
                    new Restaurant() { Id = Restaurant2Id, Name = Restaurant2Name },
                },
                new List<Pizza>()
                {
                    pizza1, pizza2, pizza3, pizza4, pizza5
                },
                new List<Customer>()
                {
                    customer1, customer2, customer3, customer4, customer5
                },
                new List<BestOfferModel>()
                {
                    new BestOfferModel() { RestaurantName = Restaurant1Name, BestPizza = "TestPizz2" },
                    new BestOfferModel() { RestaurantName = Restaurant2Name, BestPizza = "TestPizz3" },

                }
            ));
            return result;
        }
        public static List<TestCaseData> GetRestaurantAveragesData()
        {
            var result = new List<TestCaseData>();

            // Empty elements
            result.Add(new TestCaseData(
                new List<Restaurant>(),
                new List<Pizza>(),
                new List<Customer>(),
                new List<AverageModel>()
            ));

            // One brand withot car
            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name }
                },
                new List<Pizza>(),
                new List<Customer>(),
                new List<AverageModel>()
                {
                    new AverageModel() { RestaurantName = Restaurant1Name, Average = 0 }
                }
            ));

            // One brand with one car
            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name }
                },
                new List<Pizza>()
                {
                    new Pizza() { Id = 1, RestaurantId = Restaurant1Id, Name = "testPizz1", Price = 1200 }
                },
                new List<Customer>()
                {
                    new Customer() { Id = 1, PizzaId = 1, Name = "testCust1"}
                },
                new List<AverageModel>()
                {
                    new AverageModel() { RestaurantName = Restaurant1Name, Average = 1200 }
                }
            ));
            //One brand without car(bad id)
            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name }
                },
                new List<Pizza>()
                {
                    new Pizza() { Id = 1, RestaurantId = Restaurant2Id, Name = "TestPizz1", Price = 1200 }
                },
                new List<Customer>(),
                new List<AverageModel>()
                {
                    new AverageModel() { RestaurantName = Restaurant1Name, Average = 0 }
                }
            ));

            // Multiple restaurant with multiple pizza and customer
            var pizza1 = new Pizza() { Id = 1, RestaurantId = Restaurant1Id, Name = "TestPizz1", Price = 2500 };
            var pizza2 = new Pizza() { Id = 2, RestaurantId = Restaurant1Id, Name = "TestPizz2", Price = 1400 };
            var pizza3 = new Pizza() { Id = 3, RestaurantId = Restaurant2Id, Name = "TestPizz3", Price = 1000 };
            var pizza4 = new Pizza() { Id = 4, RestaurantId = Restaurant2Id, Name = "TestPizz4", Price = 2000 };
            var pizza5 = new Pizza() { Id = 5, RestaurantId = Restaurant2Id, Name = "TestPizz5", Price = 3001 };


            var customer1 = new Customer() { Id = 1, PizzaId = 1, Name = "TestCust1" };
            var customer2 = new Customer() { Id = 2, PizzaId = 1, Name = "TestCust2" };
            var customer3 = new Customer() { Id = 3, PizzaId = 1, Name = "TestCust3" };
            var customer4 = new Customer() { Id = 4, PizzaId = 2, Name = "TestCust4" };
            var customer5 = new Customer() { Id = 5, PizzaId = 2, Name = "TestCust5" };

            result.Add(new TestCaseData(
                new List<Restaurant>()
                {
                    new Restaurant() { Id = Restaurant1Id, Name = Restaurant1Name },
                    new Restaurant() { Id = Restaurant2Id, Name = Restaurant2Name },
                },
                new List<Pizza>()
                {
                    pizza1, pizza2, pizza3, pizza4, pizza5
                },
                new List<Customer>()
                {
                    customer1, customer2, customer3, customer4, customer5
                },
                new List<AverageModel>()
                {
                    new AverageModel() { RestaurantName = Restaurant1Name, Average = (double)(pizza1.Price + pizza2.Price) / 2 },
                    new AverageModel() { RestaurantName = Restaurant2Name, Average = (double)(pizza3.Price + pizza4.Price + pizza5.Price) / 3 }
                }
            ));
            return result;
        }

        #endregion
    }
}
