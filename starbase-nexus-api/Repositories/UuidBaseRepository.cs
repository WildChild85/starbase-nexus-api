using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories
{
    public abstract class UuidBaseRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : UuidBaseEntity
    {
        protected readonly MainDbContext _dbContext;
        protected readonly DbSet<EntityType> _dbSet;

        public UuidBaseRepository(MainDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<EntityType>();
        }

        public virtual async Task<EntityType> Create(EntityType entity)
        {
            EntityType entityProxy = _dbSet.CreateProxy();
            _dbContext.Entry(entityProxy).CurrentValues.SetValues(entity);
            await _dbSet.AddAsync(entityProxy);
            await _dbContext.SaveChangesAsync();
            return entityProxy;
        }

        public virtual async Task<IEnumerable<EntityType>> CreateRange(IEnumerable<EntityType> entities)
        {
            List<EntityType> entityProxies = new List<EntityType>();
            foreach (EntityType entity in entities)
            {
                EntityType entityProxy = _dbSet.CreateProxy();
                _dbContext.Entry(entityProxy).CurrentValues.SetValues(entity);
                entityProxies.Add(entityProxy);
            }
            await _dbSet.AddRangeAsync(entityProxies);
            await _dbContext.SaveChangesAsync();
            return entityProxies;
        }

        public virtual async Task Update(EntityType entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateRange(IEnumerable<EntityType> entities)
        {
            _dbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Delete(EntityType entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteRange(IEnumerable<EntityType> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<EntityType?> GetOneOrDefault(Guid id)
        {
            EntityType? entity = await _dbSet.FindAsync(id);

            return entity;
        }

        public virtual async Task<IEnumerable<EntityType>> GetMultiple(IEnumerable<Guid> ids, ShapingParameters parameters)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            IQueryable<EntityType> collection = _dbSet as IQueryable<EntityType>;

            collection = collection.Where(r => ids.Contains(r.Id));

            List<EntityType> entities = await collection.ApplySort(parameters.OrderBy).ToListAsync();

            return entities;
        }

        public virtual async Task<PagedList<EntityType>> GetMultiple(SearchParameters parameters)
        {
            IQueryable<EntityType> collection = _dbSet as IQueryable<EntityType>;

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<EntityType> pagedList = await PagedList<EntityType>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
