
using D2P0JX_SG1_21_22_2.WpfClient.BL.Interfaces;
using D2P0JX_SG1_21_22_2.WpfClient.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace D2P0JX_SG1_21_22_2.WpfClient.ViewModels
{
    class MainWindowVM : ViewModelBase
    {
        private PizzaModel currentPizza;

        public PizzaModel CurrentPizza
        {
            get { return currentPizza; }
            set { Set(ref currentPizza, value); }
        }

        public ObservableCollection<PizzaModel> Pizzas { get; private set; }

        public ICommand AddCommand { get; private set; }
        public ICommand ModifyCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ViewCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }

        readonly IPizzaHandlerService pizzaHandlerService;

        public MainWindowVM(IPizzaHandlerService pizzaHandlerService)
        {
            this.pizzaHandlerService = pizzaHandlerService;
            Pizzas = new ObservableCollection<PizzaModel>();

            if (IsInDesignMode)
            {
                Pizzas.Add(new PizzaModel(1, 2550, 30, "Margherita", 2, true));
                Pizzas.Add(new PizzaModel(2, 2325, 45, "FourSeason", 1, false));
                Pizzas.Add(new PizzaModel(3, 3451, 60, "Chili", 1, false));
                var current = new PizzaModel(4, 1101, 10, "Hawaiian", 3, false);
                Pizzas.Add(current);
                CurrentPizza = current;
            }

            LoadCommand = new RelayCommand(() =>
            {
                var pizzas = this.pizzaHandlerService.GetAll();
                Pizzas.Clear();

                foreach (var pizza in pizzas)
                {
                    Pizzas.Add(pizza);
                }
            });

            AddCommand = new RelayCommand(() => this.pizzaHandlerService.AddPizza(Pizzas));
            ModifyCommand = new RelayCommand(() => this.pizzaHandlerService.ModifyPizza(Pizzas, CurrentPizza));
            DeleteCommand = new RelayCommand(() => this.pizzaHandlerService.DeletePizza(Pizzas, CurrentPizza));
            ViewCommand = new RelayCommand(() => this.pizzaHandlerService.ViewPizza(CurrentPizza));
        }

        public MainWindowVM() : this(IsInDesignModeStatic ? null : CommonServiceLocator.ServiceLocator.Current.GetInstance<IPizzaHandlerService>())
        {

        }
    }
}
