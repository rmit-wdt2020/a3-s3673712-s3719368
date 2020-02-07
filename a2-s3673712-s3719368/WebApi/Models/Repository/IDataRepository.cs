using System.Collections.Generic;

namespace a2_s3673712_s3719368.Models.Repository
{
    public interface IDataRepository<TEntity, TKey> where TEntity : class //Tie entity with key
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(TKey id);
        TKey Add(TEntity item);
        TKey Update(TKey id, TEntity item);
        TKey Delete(TKey id);
    }
}
