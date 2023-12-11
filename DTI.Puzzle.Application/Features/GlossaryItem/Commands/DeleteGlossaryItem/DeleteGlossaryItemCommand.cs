using DTI.Puzzle.Domain.Abstractions.Messaging;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Commands.DeleteGlossaryItem
{
    public class DeleteGlossaryItemCommand : ICommand<Result>
    {
        public DeleteGlossaryItemCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
