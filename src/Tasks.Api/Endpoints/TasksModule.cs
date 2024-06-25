using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Extensions;
using Tasks.Application.Tasks.Create;
using Tasks.Application.Tasks.Delete;
using Tasks.Application.Tasks.GetAllTasks;
using Tasks.Application.Tasks.GetReleasedTasks;
using Tasks.Application.Tasks.GetTask;
using Tasks.Application.Tasks.Update;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Presentation.Tasks.Requests;

namespace Tasks.Api.Endpoints
{
    public class TasksModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var tasks = app.MapGroup("/api/tasks");

            tasks.MapPost("/", CreateTask).WithName(nameof(CreateTask));
            tasks.MapPut("/{id}", UpdateTask).WithName(nameof(UpdateTask));
            tasks.MapDelete("/{id}", DeleteTask).WithName(nameof(DeleteTask));

            tasks.MapGet("/", GetAllTasks);
            tasks.MapGet("/released", GetReleasedTasks);
            tasks.MapGet("/{id}", GetTask);
        }

        private static async Task<Results<Ok<GetAllTasksResponse[]>, ProblemHttpResult>> GetAllTasks(IMediator mediator)
        {
            var result = await mediator.Send(new GetAllTasksQuery());

            return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
        }

        private static async Task<Results<Ok<IEnumerable<GetReleasedTasksQueryResponse>>, ProblemHttpResult>>
            GetReleasedTasks(IMediator mediator)
        {
            var result = await mediator.Send(new GetReleasedTasksQuery());

            return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
        }

        private static async Task<Results<Ok<GetTaskResponse>, ProblemHttpResult>> GetTask(Guid id, IMediator mediator)
        {
            var result = await mediator.Send(new GetTaskQuery(new TaskId(id)));

            return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
        }

        private static async Task<Results<Created<CreateTaskRequest>, ProblemHttpResult>> CreateTask(
            [FromBody] CreateTaskRequest request,
            IMediator mediator)
        {
            var command = request.Adapt<CreateTaskCommand>();

            var result = await mediator.Send(command);

            return result.IsSuccess
                ? TypedResults.Created($"/api/tasks/{result.Value}", request)
                : result.ToProblemDetails();
        }

        private static async Task<Results<NoContent, ProblemHttpResult>> UpdateTask(
            Guid id,
            [FromBody] UpdateTaskRequest request,
            IMediator mediator)
        {
            var command = request.Adapt<UpdateTaskCommand>();
            command.TaskId = new TaskId(id);

            var result = await mediator.Send(command);

            return result.IsSuccess ? TypedResults.NoContent() : result.ToProblemDetails();
        }

        private static async Task<Results<NoContent, ProblemHttpResult>> DeleteTask(
            Guid id,
            IMediator mediator)
        {
            var result = await mediator.Send(new DeleteTaskCommand(new TaskId(id)));

            return result.IsSuccess ? TypedResults.NoContent() : result.ToProblemDetails();
        }
    }
}
