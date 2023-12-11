using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> Get(int id, CancellationToken cancellationToken=default);
        IQueryable<T> GetAll();
        Task<T> Add(T entity, CancellationToken cancellationToken = default);
        Task<bool> Exists(int id, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Delete(T entity);
    }
}
