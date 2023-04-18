using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.Repositories
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        #region Create methods

        TEntity Create(TEntity entity);

        Task<TEntity> CreateAsync(TEntity entity);

        IList<TEntity> CreateRange(IList<TEntity> entities);

        Task<IList<TEntity>> CreateRangeAsync(IList<TEntity> entities);

        #endregion


        #region Get methods
        Task<IList<TEntity>> GetAll();

        Task<TEntity?> GetById(int id);

        IList<TEntity> GetAllUndeleted();

        Task<TEntity?> GetByIdWithoutTracking(int id);

        IList<TEntity> Find(Func<TEntity, bool> predicate);

        IList<TEntity> FindWithTracking(Func<TEntity, bool> predicate);

        #endregion


        #region Delete methods

        Task<TEntity?> SoftDeleteAsync(TEntity entity);          

        TEntity? DeleteById(int id);

        Task<TEntity?> DeleteByIdAsync(int id);

        void DeleteRange(IList<TEntity> entities);

        void DeleteRange(IList<int> entities);

        Task DeleteRangeAsync(IList<TEntity> entities);

        Task DeleteRangeAsync(IList<int> entities);

        #endregion


        #region Update methods

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        IList<TEntity> UpdateRange(IList<TEntity> entities);

        Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entities);

        #endregion

        #region Transactions

        void CreateTransact(TEntity entity);

        Task CreateAsyncTransact(TEntity entity);

        void CreateRangeTransact(IList<TEntity> entities);

        Task CreateRangeAsyncTransact(IList<TEntity> entities);

        TEntity DeleteByIdTransact(int id);

        void DeleteTransact(TEntity entity);

        void DeleteRangeByIdsTransact(IList<int> entitiesIds);

        void DeleteRangeTransact(IList<TEntity> entities);

        void UpdateRangeTransact(IList<TEntity> entities);

        #endregion
    }
}
