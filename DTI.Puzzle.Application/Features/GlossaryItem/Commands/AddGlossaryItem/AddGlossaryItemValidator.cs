using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Commands.AddGlossaryItem
{
    public class AddGlossaryItemValidator : AbstractValidator<AddGlossaryItemCommand>
    {
        public AddGlossaryItemValidator()
        {
            RuleFor(request => request.Term).NotEmpty()
                .MinimumLength(2);
            RuleFor(request => request.Definition).NotEmpty();
        }
    }
}
