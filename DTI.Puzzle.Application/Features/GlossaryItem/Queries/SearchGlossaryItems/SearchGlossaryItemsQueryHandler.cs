using AutoMapper;
using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItems;
using DTI.Puzzle.Domain.Abstractions.Messaging;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Queries.SearchGlossaryItems
{
    public class SearchGlossaryItemsQueryHandler : IQueryHandler<SearchGlossaryItemsQuery, Maybe<GlossaryItemPaging>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SearchGlossaryItemsQueryHandler> _logger;

        public SearchGlossaryItemsQueryHandler(IUnitOfWork unitOfWork, 
                                                IMapper mapper,
                                                ILogger<SearchGlossaryItemsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Maybe<GlossaryItemPaging>> Handle(SearchGlossaryItemsQuery request, CancellationToken cancellationToken)
        {
            var validator = new SearchGlossaryItemsValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult == null || !validationResult.IsValid)
            {
                return Maybe<GlossaryItemPaging>.None;
            }
            try
            {
                var items = _unitOfWork.GlossaryItemRepository
                                           .GetAll();

                var finalSubset = items
                                .Where(x => x.IsActive &&
                                x.Term.ToLower()
                                        .Contains(request.SearchCriteria.ToLower()))
                                .OrderBy(x => x.Term);

                var subset = finalSubset
                                .Skip((request.PageNumber - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToList();

                var finalSet = subset.Select(x => _mapper.Map<GlossaryItemDto>(x))
                                        .ToList();

                return new GlossaryItemPaging(finalSubset.Count(), finalSet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return Maybe<GlossaryItemPaging>.None;
        }
    }
}
