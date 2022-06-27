using System.Linq;

namespace D2P0JX_SG1_21_22_2.Repository.Interfaces
{
    public interface IRepositoryBase<TEntity, TKey>
    {
        IQueryable<TEntity> ReadAll();
        TEntity Read(TKey id);

        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TKey id);
    }
}
