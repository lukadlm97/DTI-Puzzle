using DTI.Puzzle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Contracts
{
    public interface IHistoryChangeRepository : IGenericRepository<HistoryChange>
    {
        Task<IReadOnlyList<HistoryChange>> GetGlossaryItemChanges(int glossaryItemId,
            CancellationToken cancellationToken=default);

    }
}
