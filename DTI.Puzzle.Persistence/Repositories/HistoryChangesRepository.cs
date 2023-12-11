using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Persistence.Repositories
{

    public class HistoryChangesRepository : GenericRepository<HistoryChange>, IHistoryChangeRepository
    {

        public HistoryChangesRepository(GlossaryDbContext glossaryDbContext) 
            : base(glossaryDbContext) { }
       

        public async Task<IReadOnlyList<HistoryChange>> GetGlossaryItemChanges(int glossaryItemId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.HistoryChanges
                .Where(x => x.GlossaryItemId == glossaryItemId)
                .ToListAsync(cancellationToken);
        }
    }
}
