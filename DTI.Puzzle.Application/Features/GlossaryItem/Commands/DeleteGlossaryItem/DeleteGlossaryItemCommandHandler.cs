using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.Contracts.Errors;
using DTI.Puzzle.Application.Contracts.Errors.DataAccess;
using DTI.Puzzle.Application.Contracts.Errors.Validation;
using DTI.Puzzle.Domain.Abstractions.Messaging;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using DTI.Puzzle.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Features.GlossaryItem.Commands.DeleteGlossaryItem
{
    public class DeleteGlossaryItemCommandHandler : ICommandHandler<DeleteGlossaryItemCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteGlossaryItemCommandHandler> _logger;

        public DeleteGlossaryItemCommandHandler(IUnitOfWork unitOfWork, 
                                                ILogger<DeleteGlossaryItemCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result> Handle(DeleteGlossaryItemCommand request,
            CancellationToken cancellationToken)
        {
            var validator = new DeleteGlossaryItemValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult == null || !validationResult.IsValid)
            {
                return Result.Failure(Domain.DomainErrors.DomainErrors.GlossaryItem.GlossaryItemModel);
            }
            try
            {
                if (!await _unitOfWork.GlossaryItemRepository.Exists(request.Id, cancellationToken))
                {
                    return Result.Failure(ValidationErrors.GlossaryItem.ItemNotExist);
                }

                var item = await _unitOfWork.GlossaryItemRepository.Get(request.Id, cancellationToken);
                if (item == null)
                {
                    return Result.Failure(ValidationErrors.GlossaryItem.ItemNotExist);
                }

                item.IsActive = false;
                var newAuditItem = new HistoryChange
                {
                    GlossaryItem = item,
                    ActionId = (short)Domain.Enums.ActionEnum.Delete,
                    DateOfChanges = DateTime.UtcNow
                };

                _unitOfWork.GlossaryItemRepository.Update(item);
                await _unitOfWork.ChangeHistoryRepository.Add(newAuditItem, cancellationToken);
                await _unitOfWork.Save(cancellationToken);

                return Result.Success();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Result.Failure(DataAccessErrors.GlossaryItem.ExecutionProblem);
            }

            return Result.Failure(UnknowErrors.GlossaryItem.UnknowState);
        }
    }
}
