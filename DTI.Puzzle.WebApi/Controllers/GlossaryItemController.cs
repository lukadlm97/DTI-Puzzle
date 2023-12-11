using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.AddGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.DeleteGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.UpdateGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItemHistory;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItems;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.SearchGlossaryItems;
using DTI.Puzzle.WebApi.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DTI.Puzzle.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class GlossaryItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GlossaryItemController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application.DTOs.GlossaryItemDto>>>
            Get([FromQuery]int pageNumber = 1, 
                [FromQuery] int pageSize=10,
                CancellationToken cancellationToken = default)
        {
            if(pageNumber < 1 || pageSize < 1)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new GetGlossaryItemsQuery(pageNumber, pageSize), cancellationToken);
            if(result.HasValue)
            {
                return Ok(result.Value);
            }

            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Application.DTOs.GlossaryItemDto>>
            Get(int id,
                CancellationToken cancellationToken = default)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new GetGlossaryItemQuery(id), cancellationToken);
            if (result.HasValue)
            {
                return Ok(result.Value);
            }

            return NotFound();
        }

        [HttpGet("{id}/historyChanges")]
        public async Task<ActionResult<IEnumerable<HistoryChangesDto>>>
           GetHistoryChanges(int id,
               CancellationToken cancellationToken = default)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new GetGlossaryItemHistoryQuery(id), cancellationToken);
            if (result.HasValue)
            {
                return Ok(result.Value);
            }

            return NotFound();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Application.DTOs.GlossaryItemDto>>>
           Search([FromQuery] string query, 
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10,
               CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(query) || pageNumber < 1 || pageSize < 1)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new SearchGlossaryItemsQuery(pageNumber, pageSize, query), cancellationToken);
            if (result.HasValue)
            {
                return Ok(result.Value);
            }

            return NotFound();
        }

        [HttpPost("")]
        public async Task<ActionResult<IEnumerable<HistoryChangesDto>>>
          Add([FromBody] ViewModel.GlossaryItemViewModel request,
              CancellationToken cancellationToken = default)
        {

            var result = await _mediator
                .Send(new AddGlossaryItemCommand(request.Term, request.Definition), cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error.Message);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<IEnumerable<HistoryChangesDto>>>
         Update(int id, 
            [FromBody] ViewModel.GlossaryItemViewModel request,
             CancellationToken cancellationToken = default)
        {
            var result = await _mediator
                .Send(new UpdateGlossaryItemCommand(id, request.Term, request.Definition), cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<HistoryChangesDto>>>
         Delete(int id,
             CancellationToken cancellationToken = default)
        {
            var result = await _mediator
                .Send(new DeleteGlossaryItemCommand(id), cancellationToken);
            if (result.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest(result.Error.Message);
        }
    }
}
