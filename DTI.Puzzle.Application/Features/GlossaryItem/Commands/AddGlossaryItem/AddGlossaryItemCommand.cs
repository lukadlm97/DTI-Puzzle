using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Domain.Abstractions.Messaging;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Commands.AddGlossaryItem
{
    public class AddGlossaryItemCommand : ICommand<Result<GlossaryItemDto>>
    {
        public AddGlossaryItemCommand(string term, string definition)
        {
            Term = term;
            Definition = definition;
        }
        public string Term { get; set; }
        public string Definition { get; set; }

    }
}
