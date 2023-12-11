using DTI.Puzzle.Application.Features.GlossaryItem.Commands.DeleteGlossaryItem;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Commands.UpdateGlossaryItem
{
    public class UpdateGlossaryItemValidator : AbstractValidator<UpdateGlossaryItemCommand>
    {
        public UpdateGlossaryItemValidator()
        {
            RuleFor(request => request.Id).GreaterThan(0)
              .WithMessage(string.Format("{0} should be greater than {1}",
              nameof(DeleteGlossaryItemCommand.Id), 0));
            RuleFor(request => request.Term).NotEmpty()
               .MinimumLength(2)
                   .WithMessage("You should put at least 2 chars for term (not sound possible to one letter could have sanse)");
        }
    }
}
