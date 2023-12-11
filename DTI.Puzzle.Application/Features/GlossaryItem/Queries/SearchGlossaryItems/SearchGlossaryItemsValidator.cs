using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItems;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Queries.SearchGlossaryItems
{
    public class SearchGlossaryItemsValidator : AbstractValidator<SearchGlossaryItemsQuery>
    {
        public SearchGlossaryItemsValidator()
        {
            RuleFor(request => request.PageNumber).GreaterThan(0)
                .WithMessage(string.Format("{0} should be greater than {1}",
                nameof(GetGlossaryItemsQuery.PageNumber), 0));
            RuleFor(request => request.PageSize).GreaterThan(0)
                .WithMessage(string.Format("{0} should be greater than {1}",
                nameof(GetGlossaryItemsQuery.PageSize), 0));
            RuleFor(request => request.SearchCriteria).NotEmpty()
               .WithMessage(string.Format("Search should be performed by criteria"));
        }
    }
}
