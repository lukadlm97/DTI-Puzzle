using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Commands.DeleteGlossaryItem
{
    public class DeleteGlossaryItemValidator : AbstractValidator<DeleteGlossaryItemCommand>
    {
        public DeleteGlossaryItemValidator()
        {
            RuleFor(request => request.Id).GreaterThan(0)
                .WithMessage(string.Format("{0} should be greater than {1}",
                nameof(DeleteGlossaryItemCommand.Id), 0));
        }
    }
}
