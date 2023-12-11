using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItems;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItem
{
    public class GetGlossaryItemValidator : AbstractValidator<GetGlossaryItemQuery>
    {
        public GetGlossaryItemValidator()
        {
            RuleFor(request => request.Id).GreaterThan(0)
                .WithMessage(string.Format("{0} should be greater than {1}",
                nameof(GetGlossaryItemQuery.Id), 0));
        }
    }
}
