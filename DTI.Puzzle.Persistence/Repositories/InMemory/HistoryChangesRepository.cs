using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Persistence.Repositories.InMemory
{
    public class HistoryChangesRepository : IHistoryChangeRepository
    {
        private readonly GlossaryInMemoryDbContext _glossaryInMemoryDbContext;

        public HistoryChangesRepository(GlossaryInMemoryDbContext glossaryInMemoryDbContext)
        {
            _glossaryInMemoryDbContext = glossaryInMemoryDbContext;
        }
        public async Task<HistoryChange> Add(HistoryChange entity, CancellationToken cancellationToken = default)
        {
            var nextId = _glossaryInMemoryDbContext.DictionaryChangeHistories.Count;
            entity.Id = nextId + 1;
            entity.GlossaryItemId = entity.GlossaryItem.Id;

            _glossaryInMemoryDbContext.DictionaryChangeHistories.Add(entity);
            return entity;
        }

        public void Delete(HistoryChange entity)
        {
            var entityForRemove = _glossaryInMemoryDbContext.DictionaryChangeHistories
                .FirstOrDefault(x => x.Id == entity.Id);
            _glossaryInMemoryDbContext.DictionaryChangeHistories.Remove(entityForRemove);
        }

        public async Task<bool> Exists(int id, CancellationToken cancellationToken = default)
        {
            return _glossaryInMemoryDbContext.DictionaryChangeHistories.Any(x => x.Id == id);
        }

        public async Task<HistoryChange?> Get(int id, CancellationToken cancellationToken = default)
        {
            return _glossaryInMemoryDbContext.DictionaryChangeHistories.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<HistoryChange> GetAll()
        {

            return _glossaryInMemoryDbContext.DictionaryChangeHistories.AsQueryable();
        }

        public async Task<IReadOnlyList<HistoryChange>> GetGlossaryItemChanges(int glossaryItemId, CancellationToken cancellationToken = default)
        {
            return _glossaryInMemoryDbContext.DictionaryChangeHistories
                .Where(x => x.GlossaryItemId == glossaryItemId).ToList();
        }

        public void Update(HistoryChange entity)
        {
            var entityForRemove = _glossaryInMemoryDbContext.DictionaryChangeHistories
               .FirstOrDefault(x => x.Id == entity.Id);
            _glossaryInMemoryDbContext.DictionaryChangeHistories.Remove(entityForRemove);
            _glossaryInMemoryDbContext.DictionaryChangeHistories.Add(entity);
        }
    }
}
