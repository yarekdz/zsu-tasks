using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Extensions;
using Tasks.Application.Tasks.Create;
using Tasks.Application.Tasks.Delete;
using Tasks.Application.Tasks.GetAllTasks;
using Tasks.Application.Tasks.GetCompleteTasks;
using Tasks.Application.Tasks.GetTask;
using Tasks.Application.Tasks.Update;
using Tasks.Domain.Shared;
using Tasks.Domain.Tasks.TaskDetails;

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

        private static async Task<IResult> GetAllTasks(IMediator mediator)
        {
            var result = await mediator.Send(new GetAllTasksQuery());
            //application response => api response 

            return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
        }

        private static async Task<IResult> GetReleasedTasks(IMediator mediator)
        {
            var result = await mediator.Send(new GetReleasedTasksQuery());
            //application response => api response 

            return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
        }

        private static async Task<IResult> GetTask(Guid id, IMediator mediator)
        {
            var result = await mediator.Send(new GetTaskQuery(new TaskId(id)));
            //application response => api response 

            return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
        }

        private static async Task<IResult> CreateTask(
            [FromBody] CreateTaskCommand command,
            IMediator mediator)
        {
            //Task: request => command
            //todo: var command  = request.Adapt<CreateTaskCommand>();
            var result = await mediator.Send(command);

            return result.Match(
                onSuccess: () => TypedResults.Created($"/api/tasks{result.Value}", command),
                onFailure: _ => result.ToProblemDetails());
        }

        private static async Task<IResult> UpdateTask(Guid id,
            [FromBody] UpdateTaskCommand command,
            IMediator mediator)
        {
            //Task: request => command
            var result = await mediator.Send(command);

            return result.Match(
                onSuccess: TypedResults.NoContent,
                onFailure: _ => result.ToProblemDetails());
        }

        private static async Task<IResult> DeleteTask(Guid id, IMediator mediator)
        {
            var result = await mediator.Send(new DeleteTaskCommand(new TaskId(id)));

            return result.Match(
                onSuccess: TypedResults.NoContent,
                onFailure: _ => result.ToProblemDetails());
        }
    }
}
