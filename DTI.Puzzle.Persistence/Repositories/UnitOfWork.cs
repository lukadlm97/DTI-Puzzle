using DTI.Puzzle.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GlossaryDbContext _glossaryDbContext;
        private IGlossaryItemRepository _glossaryItemRepository;
        private IHistoryChangeRepository _dictionaryChangeHistoryRepository;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(GlossaryDbContext glossaryDbContext, ILogger<UnitOfWork> logger)
        {
            _glossaryDbContext = glossaryDbContext;
            _logger = logger;
        }

        public IGlossaryItemRepository GlossaryItemRepository =>
            _glossaryItemRepository ??= new GlossaryItemRepository(_glossaryDbContext);

        public IHistoryChangeRepository ChangeHistoryRepository =>
            _dictionaryChangeHistoryRepository ??= new HistoryChangesRepository(_glossaryDbContext);

        public void Dispose()
        {
            _glossaryDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(CancellationToken cancellationToken = default)
        {
            try
            {
                await _glossaryDbContext.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception occured on save at Unit of work", ex);
            }
         }
    }
}
