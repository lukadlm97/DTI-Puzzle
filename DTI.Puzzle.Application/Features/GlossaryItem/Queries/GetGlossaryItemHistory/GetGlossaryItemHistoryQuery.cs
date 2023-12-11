using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Domain.Abstractions.Messaging;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItemHistory
{
    public class GetGlossaryItemHistoryQuery : IQuery<Maybe<IEnumerable<HistoryChangesDto>>>
    {
        public GetGlossaryItemHistoryQuery(int glossaryId)
        {
            GlossaryId = glossaryId;
        }
        public int GlossaryId { get; set; }
    }
}
