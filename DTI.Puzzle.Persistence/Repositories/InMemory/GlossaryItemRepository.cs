using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Persistence.Repositories.InMemory
{
    public class GlossaryItemRepository : IGlossaryItemRepository
    {
        private readonly GlossaryInMemoryDbContext _glossaryInMemoryDbContext;

        public GlossaryItemRepository(GlossaryInMemoryDbContext glossaryInMemoryDbContext)
        {
            _glossaryInMemoryDbContext = glossaryInMemoryDbContext;
        }
        public async Task<GlossaryItem> Add(GlossaryItem entity, CancellationToken cancellationToken = default)
        {
            var nextId = _glossaryInMemoryDbContext.GlossaryItems.Count;
            entity.Id = nextId + 1;

            _glossaryInMemoryDbContext.GlossaryItems.Add(entity);
            return entity;
        }

        public void Delete(GlossaryItem entity)
        {
            var entityForRemove = _glossaryInMemoryDbContext.GlossaryItems
                .FirstOrDefault(x => x.Id == entity.Id);
            _glossaryInMemoryDbContext.GlossaryItems.Remove(entityForRemove);

        }

        public async Task<bool> Exists(int id, CancellationToken cancellationToken = default)
        {
            return _glossaryInMemoryDbContext.GlossaryItems.Any(x => x.Id == id);
        }

        public async Task<bool> ExistTerm(string term, CancellationToken cancellationToken = default)
        {
            return _glossaryInMemoryDbContext.GlossaryItems.Any(x => x.Term.ToLower().Contains(term.ToLower()) && x.IsActive);
        }

        public async Task<GlossaryItem?> Get(int id, CancellationToken cancellationToken = default)
        {
            return _glossaryInMemoryDbContext.GlossaryItems.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<GlossaryItem> GetAll()
        {
            return _glossaryInMemoryDbContext.GlossaryItems.AsQueryable();
        }

        public void Update(GlossaryItem entity)
        {
            var entityForRemove = _glossaryInMemoryDbContext.GlossaryItems
               .FirstOrDefault(x => x.Id == entity.Id);
            _glossaryInMemoryDbContext.GlossaryItems.Remove(entityForRemove);
            _glossaryInMemoryDbContext.GlossaryItems.Add(entity);
        }

       async Task<IReadOnlyList<GlossaryItem>> IGlossaryItemRepository.ExistTerm(string term, CancellationToken cancellationToken)
        {
            return _glossaryInMemoryDbContext.GlossaryItems
                .Where(x => x.Term.ToLower().Contains(term.ToLower()) && x.IsActive)
                .ToList();
        }
    }
}
