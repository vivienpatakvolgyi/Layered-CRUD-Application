using D2P0JX_SG1_21_22_2.Data.DbContexts;
using D2P0JX_SG1_21_22_2.Models.Entities;
using D2P0JX_SG1_21_22_2.Repository.Interfaces;
using System.Linq;

namespace D2P0JX_SG1_21_22_2.Repository.Repositories
{
    public class PizzaRepository : RepositoryBase<Pizza, int>, IPizzaRepository
    {
        public PizzaRepository(PizzaDbContext context) : base(context)
        {
        }

        public override Pizza Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
    }
}
