using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VerivoxTask.Domain.Entities;

namespace VerivoxTask.Infrastructure.Persistence.Repository
{
   
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntitytBase<long>
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<TEntity> Entities;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
            Entities = Context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {

            entity.CreatedAt = DateTime.Now;
            var result = await Entities.AddAsync(entity);           
            return result?.Entity;
        }


        public void Remove(TEntity entity)
        {
          
            Entities.Remove(entity);
        }


        public TEntity Get(int id)
        {
            return Entities.Find(id);
        }


        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        public void AddList(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.FromResult(Entities.Update(entity));
           
        }
    }
}
