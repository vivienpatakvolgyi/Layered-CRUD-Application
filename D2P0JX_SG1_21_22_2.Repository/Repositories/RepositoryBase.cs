using D2P0JX_SG1_21_22_2.Data.DbContexts;
using D2P0JX_SG1_21_22_2.Repository.Interfaces;
using System.Linq;

namespace D2P0JX_SG1_21_22_2.Repository.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        protected PizzaDbContext Context;

        protected RepositoryBase(PizzaDbContext context)
        {
            Context = context;
        }

        public abstract TEntity Read(TKey id);

        public IQueryable<TEntity> ReadAll()
        {
            return Context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            var result = Context.Add(entity);
            Context.SaveChanges();
            return result.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            var result = Context.Update(entity);
            Context.SaveChanges();
            return result.Entity;
        }

        public void Delete(TKey id)
        {
            Context.Remove(Read(id));
            Context.SaveChanges();
        }

    }
}
