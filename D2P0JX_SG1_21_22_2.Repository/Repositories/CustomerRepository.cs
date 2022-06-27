using D2P0JX_SG1_21_22_2.Data.DbContexts;
using D2P0JX_SG1_21_22_2.Models.Entities;
using D2P0JX_SG1_21_22_2.Repository.Interfaces;
using System.Linq;

namespace D2P0JX_SG1_21_22_2.Repository.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(PizzaDbContext context) : base(context)
        {
        }

        public void Delete(Restaurant entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }
        public override Customer Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
    }
}
