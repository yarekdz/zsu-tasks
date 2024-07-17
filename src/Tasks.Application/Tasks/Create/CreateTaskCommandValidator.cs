using FluentValidation;
using Tasks.Application.Validation;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.Errors;
using Tasks.Domain.Tasks;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.Tasks.Create
{
    public sealed class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        //private readonly ITaskQueriesRepository _taskQueriesRepository;

        public CreateTaskCommandValidator()
        //public CreateTaskCommandValidator(ITaskQueriesRepository taskQueriesRepository)
        {
            //_taskQueriesRepository = taskQueriesRepository;

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithError(TaskErrors.Create.InvalidTitle);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithError(TaskErrors.Create.InvalidDescription);


            //HighRisky category tasks should have  5 highest Priority
            RuleFor(x => x.Priority)
                .Must(x => x == Priority.HightestPriority)
                .When(x => x.Category == TaskCategory.HighRisky)
                .WithError(TaskErrors.Create.InvalidPriorityForHighRiskyCategory);

        }
    }
}
