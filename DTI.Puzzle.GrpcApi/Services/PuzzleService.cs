using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.AddGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.DeleteGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.UpdateGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItemHistory;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItems;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.SearchGlossaryItems;
using DTI.Puzzle.GrpcApi;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DTI.Puzzle.GrpcApi.Services
{
    public class PuzzleService : GrpcApi.PuzzleProvider.PuzzleProviderBase
    {
        private readonly ILogger<PuzzleService> _logger;
        private readonly IMediator _mediator;

        public PuzzleService(ILogger<PuzzleService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator= mediator;
        }

        public override async Task<GetAllReply> GetAll(GetAllRequest request, ServerCallContext context)
        {
            var result = await _mediator
                .Send(new GetGlossaryItemsQuery(request.PageNumber, request.PageSize), context.CancellationToken);  

            if(result.HasValue)
            {
                return new GetAllReply
                {
                    Items = { result.Value.GlossaryItems.Select(x=> new GlossaryItemReply()
                    {
                        Id = x.Id,
                        Term = x.Term,
                        Definition = x.Definition
                    }) },
                    TotalAvailable = result.Value.TotalAvailabeItems
                };
            }

            return new GetAllReply
            {
                Items = { Array.Empty<GlossaryItemReply>() },
                TotalAvailable = 0
            };
        }

        public override async Task<GlossaryItemReply> GetById(OperateByIdRequest request,
            ServerCallContext context)
        {
            var result = await _mediator.Send(new GetGlossaryItemQuery(request.Id), context.CancellationToken);

            if (result.HasValue)
            {
                return new GlossaryItemReply
                {
                    Id = result.Value.Id,
                    Term = result.Value.Term,
                    Definition = result.Value.Definition
                };
            }

            return new GlossaryItemReply
            {
                Id = -1
            };
        }

        public override async Task<GetHistoryChangesReply> GetHistoryChanges(OperateByIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new GetGlossaryItemHistoryQuery(request.Id), context.CancellationToken);
            if (result.HasValue)
            {
                return new GetHistoryChangesReply
                {
                    Items = { result.Value.Select(x=> new HistoryChangesReply()
                    {
                        Id = x.Id,
                        ActionId = x.ActionId,
                        DateOfCreation = x.DateOfCreation.ToUniversalTime().ToTimestamp()
                    }) },
                    TotalAvailable = result.Value.Count()
                };
            }

            return new GetHistoryChangesReply
            {
                Items = { Array.Empty<HistoryChangesReply>() },
                TotalAvailable = 0
            };
        }

        public override async Task<GetAllReply> Search(SearchRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new SearchGlossaryItemsQuery(request.Paging.PageNumber, request.Paging.PageSize, request.Query), context.CancellationToken);
            if (result.HasValue)
            {
                return new GetAllReply
                {
                    Items = { result.Value.GlossaryItems.Select(x=> new GlossaryItemReply()
                    {
                        Id = x.Id,
                        Term = x.Term,
                        Definition = x.Definition
                    }) },
                    TotalAvailable = result.Value.TotalAvailabeItems
                };
            }

            return new GetAllReply
            {
                Items = { Array.Empty<GlossaryItemReply>() },
                TotalAvailable = 0
            };
        }

        public override async Task<GlossaryItemReply> Add(UpsertRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new AddGlossaryItemCommand(request.Term, request.Definition), context.CancellationToken);
            if (result.IsSuccess)
            {
                return new GlossaryItemReply
                {
                   Id= result.Value.Id,
                   Term = result.Value.Term,
                   Definition = result.Value.Definition,
                };
            }

            return new GlossaryItemReply
            {
                Id = -1
            };
        }

        public override async Task<GlossaryItemReply> Update(UpsertRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new UpdateGlossaryItemCommand(request.Id, request.Term, request.Definition), context.CancellationToken);
            if (result.IsSuccess)
            {
                return new GlossaryItemReply
                {
                    Id = result.Value.Id,
                    Term = result.Value.Term,
                    Definition = result.Value.Definition,
                };
            }

            return new GlossaryItemReply
            {
                Id = -1
            };
        }

        public override async Task<GlossaryItemReply> Delete(OperateByIdRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new DeleteGlossaryItemCommand(request.Id), context.CancellationToken);
            if (result.IsSuccess)
            {
                return new GlossaryItemReply()
                {
                    Id = request.Id
                };
            }

            return new GlossaryItemReply()
            {
                Id = -1
            };
        }

    }
}
