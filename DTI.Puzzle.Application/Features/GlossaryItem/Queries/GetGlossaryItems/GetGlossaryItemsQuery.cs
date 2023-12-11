using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Domain.Abstractions.Messaging;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItems
{
    public class GetGlossaryItemsQuery : IQuery<Maybe<GlossaryItemPaging>>
    {
        public GetGlossaryItemsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;

        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
