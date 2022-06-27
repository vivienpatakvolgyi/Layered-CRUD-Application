using D2P0JX_SG1_21_22_2.Models.Entities;

namespace D2P0JX_SG1_21_22_2.Repository.Interfaces
{
    public interface ICustomerRepository : IRepositoryBase<Customer, int>
    {
        void Delete(Restaurant entity);
    }
}
