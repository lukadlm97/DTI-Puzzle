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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GlossaryInMemoryDbContext _glossaryInMemoryDbContext;
        private IGlossaryItemRepository _glossaryItemRepository;
        private IHistoryChangeRepository _dictionaryChangeHistoryRepository;

        public UnitOfWork(GlossaryInMemoryDbContext glossaryInMemoryDbContext)
        {
            _glossaryInMemoryDbContext = glossaryInMemoryDbContext;
        }
        public IGlossaryItemRepository GlossaryItemRepository =>
            _glossaryItemRepository ??= new GlossaryItemRepository(_glossaryInMemoryDbContext);

        public IHistoryChangeRepository ChangeHistoryRepository =>
            _dictionaryChangeHistoryRepository ??=
            new HistoryChangesRepository(_glossaryInMemoryDbContext);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task Save(CancellationToken cancellationToken = default)
        {
        }
    }
}
