using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Domain.Abstractions.Messaging;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Commands.UpdateGlossaryItem
{
    public class UpdateGlossaryItemCommand : ICommand<Result<GlossaryItemDto>>
    {
        public UpdateGlossaryItemCommand(int id, string term, string definition)
        {
            Id = id;
            Term = term;
            Definition = definition;
        }
        public int Id { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
    }
}
