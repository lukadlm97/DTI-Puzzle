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

namespace DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItem
{
    public class GetGlossaryItemQueryHandler : IQueryHandler<GetGlossaryItemQuery, Maybe<GlossaryItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetGlossaryItemQueryHandler> _logger;

        public GetGlossaryItemQueryHandler(IUnitOfWork unitOfWork,
                                            IMapper mapper, 
                                            ILogger<GetGlossaryItemQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Maybe<GlossaryItemDto>> Handle(GetGlossaryItemQuery request, 
            CancellationToken cancellationToken)
        {
            var validator = new GetGlossaryItemValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult == null || !validationResult.IsValid)
            {
                return Maybe<GlossaryItemDto>.None;
            }
            try
            {
                var item  = await _unitOfWork.GlossaryItemRepository
                                            .Get(request.Id, cancellationToken);

                if(item == null)
                {
                    return Maybe<GlossaryItemDto>.None;
                }

                if (item.IsActive)
                {
                    return _mapper.Map<GlossaryItemDto>(item);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return Maybe<GlossaryItemDto>.None;
        }
    }
}
