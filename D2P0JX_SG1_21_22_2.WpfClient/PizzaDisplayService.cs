using D2P0JX_SG1_21_22_2.WpfClient.BL.Interfaces;
using D2P0JX_SG1_21_22_2.WpfClient.Models;

namespace D2P0JX_SG1_21_22_2.WpfClient
{
    class PizzaDisplayService : IPizzaDisplayService
    {
        public void Display(PizzaModel pizza)
        {
            var window = new PizzaEditorWindow(pizza, false);

            window.Show();
        }
    }
}
