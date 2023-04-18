using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public AuthenticationDbContext Context;

        public DbSet<TEntity> DbSet { get; }

        public GenericRepository(AuthenticationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        #region Save methods
        public void Save() => Context.SaveChanges();

        public async Task SaveAsync() => await Context.SaveChangesAsync();

        #endregion

        #region Create methods
        public TEntity Create(TEntity entity)
        {
            CreateTransact(entity);
            Save();
            return entity;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            CreateAsyncTransact(entity);
            await SaveAsync();
            return entity;
        }

        public IList<TEntity> CreateRange(IList<TEntity> entities)
        {            
            CreateRangeTransact(entities);
            Save();
            return entities;
        }

        public async Task<IList<TEntity>> CreateRangeAsync(IList<TEntity> entities)
        {
            CreateRangeAsyncTransact(entities);
            await SaveAsync();
            return entities;
        }

        #endregion

        #region Get methods
        public async Task<IList<TEntity>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public IList<TEntity> GetAllUndeleted()
        {
            var obj = Expression.Parameter(typeof(TEntity), typeof(TEntity).Name);

            var searchValue = Expression.Constant(false, typeof(bool));

            var objProperty = Expression.PropertyOrField(obj, "IsDeleted");

            var expression = Expression.Equal(objProperty, searchValue);

            var lambda = Expression.Lambda<Func<TEntity, bool>>(expression, obj);

            var compiledLambda = lambda.Compile();

            var searchResult = DbSet.AsNoTracking().Where(compiledLambda).ToList();

            return searchResult;
        }

        public async Task<TEntity?> GetByIdWithoutTracking(int id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity != null)
            {
                Context.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            return null;
        }

        public IList<TEntity> Find(Func<TEntity, bool> predicate)
        {
            var entity = DbSet.AsNoTracking().Where(predicate).ToList();

            return entity;
        }

        public IList<TEntity> FindWithTracking(Func<TEntity, bool> predicate)
        {
            var entity = DbSet.Where(predicate).ToList();

            return entity;
        }

        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                    //Context = null;
                }
            }
        }
        #endregion

        #region Delete methods

        public async Task<TEntity?> SoftDeleteAsync(TEntity entity)
        {
            dynamic dynamicEntity = entity;

            if (dynamicEntity != null)
            {
                dynamicEntity.IsDeleted = true;

                await SaveAsync();

                return dynamicEntity;
            }
            return dynamicEntity;

        }

        public TEntity? DeleteById(int id)
        {
            var entity = DeleteByIdTransact(id);
           
            Save();
            
            return entity;
        }

        public async Task<TEntity?> DeleteByIdAsync(int id)
        {
            var entity = DeleteByIdTransact(id);

            await SaveAsync();

            return entity;    
        }
      

        public void DeleteRange(IList<TEntity> entities)
        {
            DeleteRangeTransact(entities);

            Save();           
        }

        public void DeleteRange(IList<int> entities)
        {
            DeleteRangeByIdsTransact(entities);

            Save();
        }

        public async Task DeleteRangeAsync(IList<TEntity> entities)
        {
             DeleteRangeTransact(entities);

            await SaveAsync();
        }

        public async Task DeleteRangeAsync(IList<int> entities)
        {
            DeleteRangeByIdsTransact(entities);

            await SaveAsync();
        }

        #endregion

        #region Update methods

        public TEntity Update(TEntity entity)
        {
            Context.ChangeTracker.TrackGraph(entity, e => e.Entry.State = EntityState.Modified);

            Save();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.ChangeTracker.TrackGraph(entity, e => e.Entry.State = EntityState.Modified);

            await SaveAsync();

            return entity;
        }

        public IList<TEntity> UpdateRange(IList<TEntity> entities)
        {
            UpdateRangeTransact(entities);            

            Save();

            return entities;
        }

        public async Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entities)
        {
            UpdateRangeAsync(entities);            

            await SaveAsync();

            return entities;
        }

        #endregion

        #region Transactions

        public void CreateTransact(TEntity entity)
        {
            Context.Add(entity);            
        }

        public async Task CreateAsyncTransact(TEntity entity)
        {
            await Context.AddAsync(entity);
        }

        public void CreateRangeTransact(IList<TEntity> entities)
        {
            Context.AddRange(entities);
        }

        public async Task CreateRangeAsyncTransact(IList<TEntity> entities)
        {
            await Context.AddRangeAsync(entities);
        }

        public TEntity DeleteByIdTransact(int id)
        {
            var entity = DbSet.Find(id);

            if(entity != null) 
                Context.Remove(entity);
            return entity;
        }

        public void DeleteTransact(TEntity entity)
        {
            if(entity != null)            
                Context.Remove(entity);            
        }

        public void DeleteRangeByIdsTransact(IList<int> entitiesIds)
        {
            if (entitiesIds.Count > 0)
                Context.RemoveRange(entitiesIds);
        }

        public void DeleteRangeTransact(IList<TEntity> entities)
        {
            if(entities.Count > 0)
                Context.RemoveRange(entities);
        }

        public void UpdateRangeTransact(IList<TEntity> entities)
        {
            entities.ToList().ForEach(entity => Context.Entry(entity).State = EntityState.Modified);
        }

        #endregion
    }
}
