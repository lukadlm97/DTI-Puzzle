using AutoMapper;
using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.Contracts.Errors;
using DTI.Puzzle.Application.Contracts.Errors.DataAccess;
using DTI.Puzzle.Application.Contracts.Errors.Validation;
using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Application.Utilities;
using DTI.Puzzle.Domain.Abstractions.Messaging;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using DTI.Puzzle.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Commands.AddGlossaryItem
{
    public class AddGlossaryItemCommandHandler : ICommandHandler<AddGlossaryItemCommand, Result<GlossaryItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddGlossaryItemCommandHandler> _logger;

        public AddGlossaryItemCommandHandler(IUnitOfWork unitOfWork, 
                                                IMapper mapper, 
                                                ILogger<AddGlossaryItemCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<GlossaryItemDto>> Handle(AddGlossaryItemCommand request,
            CancellationToken cancellationToken)
        {
            var validator = new AddGlossaryItemValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult == null || !validationResult.IsValid)
            {
                return Result
                    .Failure<GlossaryItemDto>(Domain.DomainErrors.DomainErrors.GlossaryItem.GlossaryItemModel);
            }
            try
            {
                if(_unitOfWork.GlossaryItemRepository
                                    .GetAll()
                                    .IsExactTermInGlossary(request.Term))
                {
                    return Result
                            .Failure<GlossaryItemDto>(ValidationErrors.GlossaryItem.TermExist);

                }
                
                var newItem = new Domain.Entities.GlossaryItem()
                {
                    Term = request.Term,
                    Definition = request.Definition
                };

                var auditItem = new Domain.Entities.HistoryChange()
                {
                    GlossaryItem = newItem,
                    ActionId = (short)Domain.Enums.ActionEnum.Create,
                    DateOfChanges = DateTime.UtcNow
                };

                await _unitOfWork.GlossaryItemRepository.Add(newItem, cancellationToken);
                await _unitOfWork.ChangeHistoryRepository.Add(auditItem, cancellationToken);

                await _unitOfWork.Save(cancellationToken);

                return Result<GlossaryItemDto>
                    .Success(_mapper.Map<GlossaryItemDto>(newItem));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Result
                  .Failure<GlossaryItemDto>(DataAccessErrors.GlossaryItem.ExecutionProblem);
            }
            
            return Result.Failure<GlossaryItemDto>(UnknowErrors.GlossaryItem.UnknowState);
        }
    }
}
