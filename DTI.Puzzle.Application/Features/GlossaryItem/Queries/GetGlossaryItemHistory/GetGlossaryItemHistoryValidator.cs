using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItemHistory
{
    public class GetGlossaryItemHistoryValidator : AbstractValidator<GetGlossaryItemHistoryQuery>
    {
        public GetGlossaryItemHistoryValidator()
        {
            RuleFor(request => request.GlossaryId)
                .GreaterThan(0)
                .WithMessage(string.Format("{0} should be greater than {1}",
                    nameof(GetGlossaryItemHistoryQuery.GlossaryId), 0));
        }
    }
}
