using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Extensions;
using Tasks.Application.Tasks.Create;
using Tasks.Application.Tasks.Delete;
using Tasks.Application.Tasks.Estimate;
using Tasks.Application.Tasks.GetAllTasks;
using Tasks.Application.Tasks.GetReleasedTasks;
using Tasks.Application.Tasks.GetTask;
using Tasks.Application.Tasks.StartWork;
using Tasks.Application.Tasks.Update;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Presentation.Tasks.Requests;
using Tasks.Presentation.Tasks.Responses;

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
            tasks.MapPost("/{id}/startWork", StartWork).WithName(nameof(StartWork));
            tasks.MapPost("/{id}/estimate", Estimate).WithName(nameof(Estimate));

            tasks.MapGet("/", GetAllTasks);
            tasks.MapGet("/released", GetReleasedTasks);
            tasks.MapGet("/{id}", GetTask);
            tasks.MapGet("/{id}/history", GetTaskHistory);
        }

        private static async Task<Results<Ok<TaskResponseView[]>, ProblemHttpResult>> GetAllTasks(IMediator mediator)
        {
            var result = await mediator.Send(new GetAllTasksQuery());

            return result.IsSuccess
                ? TypedResults.Ok(result.Value.Adapt<TaskResponseView[]>())
                : result.ToProblemDetails();
        }

        private static async Task<Results<Ok<IEnumerable<TaskResponseView>>, ProblemHttpResult>>
            GetReleasedTasks(IMediator mediator)
        {
            var result = await mediator.Send(new GetReleasedTasksQuery());

            return result.IsSuccess
                ? TypedResults.Ok(result.Value.Adapt<IEnumerable<TaskResponseView>>())
                : result.ToProblemDetails();
        }

        private static async Task<Results<Ok<TaskDetailsResponseView>, ProblemHttpResult>> GetTask(Guid id,
            IMediator mediator)
        {
            var getTaskResult = await mediator.Send(new GetTaskQuery(new TaskId(id)));

            return getTaskResult.IsSuccess
                ? TypedResults.Ok(getTaskResult.Value.Adapt<TaskDetailsResponseView>())
                : getTaskResult.ToProblemDetails();
        }

        private static async Task<Results<Ok<IEnumerable<TaskHistoryView>>, ProblemHttpResult>>
            GetTaskHistory(Guid id, IMediator mediator)
        {
            var result = await mediator.Send(new GetTaskHistoryQuery(new TaskId(id)));

            return result.IsSuccess
                ? TypedResults.Ok(result.Value.Adapt<IEnumerable<TaskHistoryView>>())
                : result.ToProblemDetails();
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

        private static async Task<Results<Ok<EstimateResponseView>, ProblemHttpResult>> Estimate(
            Guid id,
            [FromBody] EstimateRequest request,
            IMediator mediator)
        {
            var command = request.Adapt<EstimateCommand>();
            command.TaskId = new TaskId(id);

            var result = await mediator.Send(command);

            return result.IsSuccess
                ? TypedResults.Ok(result.Value.Adapt<EstimateResponseView>())
                : result.ToProblemDetails();
        }

        private static async Task<Results<NoContent, ProblemHttpResult>> StartWork(
            Guid id,
            [FromBody] StartWorkRequest request,
            IMediator mediator)
        {
            var command = request.Adapt<StartWorkCommand>();
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
