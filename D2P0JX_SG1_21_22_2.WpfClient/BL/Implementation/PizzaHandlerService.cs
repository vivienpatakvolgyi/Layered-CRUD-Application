using D2P0JX_SG1_21_22_2.Models.DTOs;
using D2P0JX_SG1_21_22_2.Models.Entities;
using D2P0JX_SG1_21_22_2.WpfClient.BL.Interfaces;
using D2P0JX_SG1_21_22_2.WpfClient.Infrastructure;
using D2P0JX_SG1_21_22_2.WpfClient.Models;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace D2P0JX_SG1_21_22_2.WpfClient.BL.Implementation
{
    class PizzaHandlerService : IPizzaHandlerService
    {
        readonly IMessenger messenger;
        readonly IPizzaEditorService editorService;
        readonly IPizzaDisplayService pizzaDisplayService;
        HttpService httpService;

        public PizzaHandlerService(IMessenger messenger, IPizzaEditorService editorService, IPizzaDisplayService pizzaDisplayService)
        {
            this.messenger = messenger;
            this.editorService = editorService;
            this.pizzaDisplayService = pizzaDisplayService;
            httpService = new HttpService("Pizza", "http://localhost:30944/api/");
        }

        public void AddPizza(IList<PizzaModel> collection)
        {
            PizzaModel pizzaToEdit = null;
            bool operationFinished = false;

            do
            {
                var newPizza = editorService.EditPizza(pizzaToEdit);

                if (newPizza != null)
                {
                    var operationResult = httpService.Create(new PizzaDTO()
                    {
                        RestaurantId = newPizza.RestaurantId,
                        Name = newPizza.Name,
                        Id = newPizza.Id,
                        Price = newPizza.Price,
                        Size = newPizza.Size,
                        IsGlutenFree = newPizza.IsGlutenFree


                    }) ;

                    pizzaToEdit = newPizza;
                    operationFinished = operationResult.IsSuccess;

                    if (operationResult.IsSuccess)
                    {
                        RefreshCollectionFromServer(collection);

                        SendMessage("Pizza add was successful");
                    }
                    else
                    {

                        //SendMessage(operationResult.Messages.ToArray());
                        SendMessage("Please enter valid value!");
                    }
                }
                else
                {
                    SendMessage("Pizza add has cancelled");
                    operationFinished = true;
                }
            } while (!operationFinished);
        }

        private void RefreshCollectionFromServer(IList<PizzaModel> collection)
        {
            collection.Clear();
            var newPizzas = GetAll();

            foreach (var pizza in newPizzas)
            {
                collection.Add(pizza);
            }
        }

        private void SendMessage(params string[] messages)
        {
            messenger.Send(messages, "BlOperationResult");
        }


        public void ModifyPizza(IList<PizzaModel> collection, PizzaModel pizza)
        {
            if (pizza == null)
            {
                MessageBox.Show("Please choose a pizza first to edit.", "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {

                PizzaModel pizzaToEdit = pizza;
                bool operationFinished = false;

                do
                {
                    var editedPizza = editorService.EditPizza(pizzaToEdit);

                    if (editedPizza != null)
                    {
                        var operationResult = httpService.Update(new PizzaDTO()
                        {
                            Id = editedPizza.Id,
                            Price = editedPizza.Price,
                            Size = editedPizza.Size,
                            RestaurantId = editedPizza.RestaurantId,
                            Name = editedPizza.Name,
                            IsGlutenFree = editedPizza.IsGlutenFree
                        });

                        pizzaToEdit = editedPizza;
                        operationFinished = operationResult.IsSuccess;

                        if (operationResult.IsSuccess)
                        {
                            RefreshCollectionFromServer(collection);
                            SendMessage("Pizza modification was successful");
                        }
                        else
                        {
                            SendMessage(operationResult.Messages.ToArray());
                        }
                    }
                    else
                    {
                        SendMessage("Pizza modification has cancelled");
                        operationFinished = true;
                    }
                } while (!operationFinished);
            }
        }
        public void DeletePizza(IList<PizzaModel> collection, PizzaModel pizza)
        {
            if (pizza != null)
            {
                var operationResult = httpService.Delete(pizza.Id);

                if (operationResult.IsSuccess)
                {
                    RefreshCollectionFromServer(collection);
                    SendMessage("Pizza deletion was successful");
                }
                else
                {
                    SendMessage(operationResult.Messages.ToArray());
                }
            }
            else
            {
                SendMessage("Please choose a pizza first to delete.");
            }
        }




        public IList<PizzaModel> GetAll()
        {
            var pizzas = httpService.GetAll<Pizza>();

            return pizzas.Select(x => new PizzaModel(x.Id, x.Price, x.Size, x.Name, x.RestaurantId, x.IsGlutenFree)).ToList();
        }
        public IList<RestaurantModel> GetAllRestaurants()
        {
            var restaurants = new List<RestaurantModel>()
            {
                new RestaurantModel(1,"DonPepe"),
                new RestaurantModel(2,"Ristorante"),
                new RestaurantModel(3,"PizzaEataliano"),
            };
            return restaurants;
        }

        public IList<CustomerModel> GetAllCustomers()
        {
            var customers = new List<CustomerModel>()
            {
                new CustomerModel(1, "Nagy Julianna", 1),
                new CustomerModel(2, "Kiss Tamás", 1),
                new CustomerModel(3, "Novák Petra", 3),
            };
            return customers;
        }

        public void ViewPizza(PizzaModel pizza)
        {
            if (pizza == null)
            {
                MessageBox.Show("Please choose a pizza first.");
                return;
            }
            else
            {
                pizzaDisplayService.Display(pizza);
            }

        }
    }
}
