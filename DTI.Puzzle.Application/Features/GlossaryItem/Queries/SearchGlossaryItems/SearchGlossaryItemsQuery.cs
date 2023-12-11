using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Domain.Abstractions.Messaging;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Queries.SearchGlossaryItems
{
    public class SearchGlossaryItemsQuery : IQuery<Maybe<GlossaryItemPaging>>
    {
        public SearchGlossaryItemsQuery(int pageNumber, int pageSize, string searchCriteria)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchCriteria = searchCriteria;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchCriteria { get; private set; }
    }
}
