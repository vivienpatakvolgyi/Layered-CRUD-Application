using CommonServiceLocator;
using D2P0JX_SG1_21_22_2.WpfClient.BL.Interfaces;
using D2P0JX_SG1_21_22_2.WpfClient.Models;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;

namespace D2P0JX_SG1_21_22_2.WpfClient.ViewModels
{
    class PizzaEditorVM : ViewModelBase
    {
        private PizzaModel currentPizza;

        public PizzaModel CurrentPizza
        {
            get { return currentPizza; }
            set
            {
                Set(ref currentPizza, value);
                SelectedRestaurant = AvailableRestaurants?.SingleOrDefault(x => x.Id == CurrentPizza.RestaurantId);
            }
        }

        private RestaurantModel restaurantModel;

        public RestaurantModel SelectedRestaurant
        {
            get { return restaurantModel; }
            set
            {
                Set(ref restaurantModel, value);
                currentPizza.RestaurantId = restaurantModel?.Id ?? 0;
            }
        }

        public IList<RestaurantModel> AvailableRestaurants { get; set; }

        private bool editEnabled;
        public bool EditEnabled
        {
            get { return editEnabled; }
            set
            {
                Set(ref editEnabled, value);
                RaisePropertyChanged(nameof(CancelButtonVisibility));
            }
        }

        public System.Windows.Visibility CancelButtonVisibility => 
            EditEnabled ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

        public PizzaEditorVM(IPizzaHandlerService pizzaHandlerService)
        {
            CurrentPizza = new PizzaModel();

            if (IsInDesignMode)
            {
                AvailableRestaurants = new List<RestaurantModel>()
                {
                    new RestaurantModel(1,"DonPepe"),
                    new RestaurantModel(2,"PizzaHut"),
                    new RestaurantModel(3,"PizzaMe")
                };

                SelectedRestaurant = AvailableRestaurants[1];
                CurrentPizza.Name = "current";
                CurrentPizza.Price = 4000;
                CurrentPizza.Size = 30;
                CurrentPizza.IsGlutenFree = true;
            }
            else
            {
                AvailableRestaurants = pizzaHandlerService.GetAllRestaurants();
            }
        }

        public PizzaEditorVM() : this(IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IPizzaHandlerService>())
        {

        }
    }
}
