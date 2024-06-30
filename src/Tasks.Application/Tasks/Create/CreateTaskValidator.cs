using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Domain.Errors;
using Tasks.Domain.Tasks;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.Tasks.Create
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
    {
        //private readonly ITaskQueriesRepository _taskQueriesRepository;

        public CreateTaskValidator()
        //public CreateTaskValidator(ITaskQueriesRepository taskQueriesRepository)
        {
            //_taskQueriesRepository = taskQueriesRepository;

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithErrorCode(TaskErrors.Create.InvalidTitle.Code)
                .WithMessage(TaskErrors.Create.InvalidTitle.Message);

            RuleFor(x => x.Description)
                .NotEmpty();

            //HighRisky category tasks should have  5 highest Priority
            RuleFor(x => x.Priority)
                .Must(x => x == Priority.HightestPriority)
                .When(x => x.Category == TaskCategory.HighRisky);
            //todo: work with Errors

        }
    }
}
