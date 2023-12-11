using DTI.Puzzle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Contracts
{
    public interface IGlossaryItemRepository : IGenericRepository<GlossaryItem>
    {
        Task<IReadOnlyList<GlossaryItem>> ExistTerm(string term, CancellationToken cancellationToken = default);
    }
}
