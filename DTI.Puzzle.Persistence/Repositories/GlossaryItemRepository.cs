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
    public class GlossaryItemRepository : GenericRepository<GlossaryItem>,
        IGlossaryItemRepository
    {

        public GlossaryItemRepository(GlossaryDbContext glossaryDbContext) : base(glossaryDbContext) { }

        public async Task<IReadOnlyList<GlossaryItem>> ExistTerm(string term, CancellationToken cancellationToken = default)
        {
            return await _dbContext.GlossaryItems
                .Where(x=> x.Term.ToLower() == term.ToLower() && x.IsActive)
                .ToListAsync(cancellationToken);
        }
    }
}
