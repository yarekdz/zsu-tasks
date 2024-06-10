using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Extensions;
using Tasks.Application.Tasks.Create;
using Tasks.Application.Tasks.GetTask;
using Tasks.Domain.Shared;

namespace Tasks.Api.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ILogger<TasksController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [MapToApiVersion(1)]
        [HttpGet("{id:guid}", Name = "GetTaskInfo")]
        [Produces("application/json")]
        public async Task<IResult> GetTaskInfo(Guid id)
        {
            var result = await _mediator.Send(new GetTaskQuery(id));

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }

        [HttpPost()]
        [Produces("application/json")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            //todo: request => command
            var result = await _mediator.Send(command);

            //todo: result => response.MapToResponse()

            //todo: remove BadRequest => ese endpoints IResult
            return result.Match<IActionResult>(
                onSuccess: () => CreatedAtAction(nameof(GetTaskInfo), new { id = result.Value }, result.Value),
                onFailure: error => BadRequest(result.ToProblemDetails()));
        }
    }
}
