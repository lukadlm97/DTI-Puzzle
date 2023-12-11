using AutoMapper;
using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Domain.Abstractions.Messaging;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItems
{
    public class GetGlossaryItemsQueryHandler : 
        IQueryHandler<GetGlossaryItemsQuery, Maybe<GlossaryItemPaging>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetGlossaryItemsQueryHandler> _logger;

        public GetGlossaryItemsQueryHandler(IUnitOfWork unitOfWork, 
                                            IMapper mapper, 
                                            ILogger<GetGlossaryItemsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Maybe<GlossaryItemPaging>> Handle(GetGlossaryItemsQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetGlossaryItemsValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult == null ||  !validationResult.IsValid)
            {
                return Maybe<GlossaryItemPaging>.None;
            }
            try
            {
                var items = _unitOfWork.GlossaryItemRepository
                                           .GetAll()
                                           .Where(x => x.IsActive);

                var subset = items
                                .OrderBy(x => x.Term)
                                .Skip((request.PageNumber - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToList();

                var finalSet = subset
                                .Select(x => _mapper.Map<GlossaryItemDto>(x))
                                .ToList();

                return new GlossaryItemPaging(items.Count(), finalSet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return Maybe<GlossaryItemPaging>.None;
        }
    }
}
