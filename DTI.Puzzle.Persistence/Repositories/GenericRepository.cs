using DTI.Puzzle.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly GlossaryDbContext _dbContext;

        public GenericRepository(GlossaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> Add(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<T>> Add(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddRangeAsync(entities, cancellationToken);
            return entities;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<bool> Exists(int id, CancellationToken cancellationToken = default)
        {
            var entity = await Get(id, cancellationToken);
            return entity != null;
        }

        public async Task<T?> Get(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.FindAsync<T>(id, cancellationToken);
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
