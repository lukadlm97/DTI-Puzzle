using AutoMapper;
using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.Contracts.Errors;
using DTI.Puzzle.Application.Contracts.Errors.DataAccess;
using DTI.Puzzle.Application.Contracts.Errors.Validation;
using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.DeleteGlossaryItem;
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

namespace DTI.Puzzle.Application.Features.GlossaryItem.Commands.UpdateGlossaryItem
{
    public class UpdateGlossaryItemCommandHandler :
        ICommandHandler<UpdateGlossaryItemCommand, Result<GlossaryItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateGlossaryItemCommandHandler> _logger;

        public UpdateGlossaryItemCommandHandler(IUnitOfWork unitOfWork,
                                                IMapper mapper, 
                                                ILogger<UpdateGlossaryItemCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<GlossaryItemDto>> Handle(UpdateGlossaryItemCommand request, 
            CancellationToken cancellationToken)
        {
            var validator = new UpdateGlossaryItemValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult == null || !validationResult.IsValid)
            {
                return Result
                    .Failure<GlossaryItemDto>(Domain.DomainErrors.DomainErrors.GlossaryItem.GlossaryItemModel);
            }
            try
            {
                if (!await _unitOfWork.GlossaryItemRepository.Exists(request.Id, cancellationToken))
                {
                    return Result
                        .Failure<GlossaryItemDto>(ValidationErrors.GlossaryItem.ItemNotExist);
                }

                var item = await _unitOfWork.GlossaryItemRepository.Get(request.Id, cancellationToken);
                if (item == null)
                {
                    return Result
                        .Failure<GlossaryItemDto>(ValidationErrors.GlossaryItem.ItemNotExist);
                }
                if(item.Term.ToLower() != request.Term.ToLower() &&
                    _unitOfWork.GlossaryItemRepository.GetAll().IsExactTermInGlossary(request.Term))
                {
                    return Result
                        .Failure<GlossaryItemDto>(ValidationErrors.GlossaryItem.TermExist);
                }
                if (_unitOfWork.GlossaryItemRepository.GetAll()
                                                        .IsExactOneTermInGlossary(request.Term))
                {
                    return Result
                        .Failure<GlossaryItemDto>(ValidationErrors.GlossaryItem.TermExist);
                }

                item.Term = request.Term;
                item.Definition = request.Definition;
                var newAuditItem = new HistoryChange
                {
                    GlossaryItem = item,
                    ActionId = (short)Domain.Enums.ActionEnum.Update,
                    DateOfChanges = DateTime.UtcNow
                };

                _unitOfWork.GlossaryItemRepository.Update(item);
                await _unitOfWork.ChangeHistoryRepository.Add(newAuditItem, cancellationToken);
                await _unitOfWork.Save(cancellationToken);

                return Result.Success(_mapper.Map<GlossaryItemDto>(item));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Result
                    .Failure<GlossaryItemDto>(DataAccessErrors.GlossaryItem.ExecutionProblem);
            }

            return Result
                .Failure<GlossaryItemDto>(UnknowErrors.GlossaryItem.UnknowState);
        }
    }
}