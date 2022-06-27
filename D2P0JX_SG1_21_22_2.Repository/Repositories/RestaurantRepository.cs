using D2P0JX_SG1_21_22_2.Data.DbContexts;
using D2P0JX_SG1_21_22_2.Models.Entities;
using D2P0JX_SG1_21_22_2.Repository.Interfaces;
using System.Linq;

namespace D2P0JX_SG1_21_22_2.Repository.Repositories
{
    public class RestaurantRepository : RepositoryBase<Restaurant, int>, IRestaurantRepository
    {
        public RestaurantRepository(PizzaDbContext context) : base(context)
        {
        }

        public void Delete(Restaurant entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }

        public override Restaurant Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
    }
}
