using AutoMapper;
using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItem;
using DTI.Puzzle.Domain.Abstractions.Messaging;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItemHistory
{
    public class GetGlossaryItemHistoryQueryHandler : IQueryHandler<GetGlossaryItemHistoryQuery, Maybe<IEnumerable<HistoryChangesDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetGlossaryItemHistoryQueryHandler> _logger;

        public GetGlossaryItemHistoryQueryHandler(IUnitOfWork unitOfWork,
                                                    IMapper mapper, 
                                                    ILogger<GetGlossaryItemHistoryQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Maybe<IEnumerable<HistoryChangesDto>>> Handle(GetGlossaryItemHistoryQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetGlossaryItemHistoryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult == null || !validationResult.IsValid)
            {
                return Maybe<IEnumerable<HistoryChangesDto>>.None;
            }

            try
            {
                var items = await _unitOfWork.ChangeHistoryRepository
                                .GetGlossaryItemChanges(request.GlossaryId, cancellationToken);
                if (items != null && items.Any())
                {
                    return items
                        .Select(x => _mapper.Map<HistoryChangesDto>(x))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return Maybe<IEnumerable<HistoryChangesDto>>.None;
        }
    }
}
