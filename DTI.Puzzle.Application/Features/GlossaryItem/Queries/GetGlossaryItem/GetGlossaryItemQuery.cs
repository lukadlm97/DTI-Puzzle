using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Domain.Abstractions.Messaging;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItem
{
    public class GetGlossaryItemQuery : IQuery<Maybe<GlossaryItemDto>>
    {
        public GetGlossaryItemQuery(int id)
        {
            Id = id;

        }
        public int Id { get; set; }
    }
}
