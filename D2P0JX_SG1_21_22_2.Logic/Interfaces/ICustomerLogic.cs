using D2P0JX_SG1_21_22_2.Models.Entities;
using System.Collections.Generic;

namespace D2P0JX_SG1_21_22_2.Logic.Interfaces
{
    public interface ICustomerLogic
    {
        IList<Customer> ReadAll();
        Customer Read(int id);
        Customer Create(Customer entity);
        Customer Update(Customer entity);
        void Delete(int id);
        List<Customer> ReadAllByPizzaId(int customerId);
    }
}
