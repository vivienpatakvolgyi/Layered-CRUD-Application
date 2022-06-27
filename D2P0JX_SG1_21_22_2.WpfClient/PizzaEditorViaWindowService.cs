using D2P0JX_SG1_21_22_2.WpfClient.BL.Interfaces;
using D2P0JX_SG1_21_22_2.WpfClient.Models;

namespace D2P0JX_SG1_21_22_2.WpfClient
{
    class PizzaEditorViaWindowService : IPizzaEditorService
    {
        public PizzaModel EditPizza(PizzaModel pizza)
        {
            var window = new PizzaEditorWindow(pizza);

            if (window.ShowDialog() == true)
            {
                return window.Pizza;
            }
            return null;
        }
    }
}
