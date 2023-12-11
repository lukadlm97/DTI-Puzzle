using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IGlossaryItemRepository GlossaryItemRepository { get; }
        IHistoryChangeRepository ChangeHistoryRepository { get; }
        Task Save(CancellationToken cancellationToken = default);
    }
}
