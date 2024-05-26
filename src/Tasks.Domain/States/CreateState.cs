using Tasks.Domain.Shared;
using Tasks.Domain.Tasks;

namespace Tasks.Domain.States;

public sealed class CreateState : State
{

    public override string Title => "Create";

    public CreateState(TodoTask todoTask) : base(todoTask)
    {
    }

    public override Result DoStateWork()
    {
        if (TodoTask == null)
        {
            return Result.Failure(DomainErrors.TaskErrors.NullTask);
        }

        if (string.IsNullOrEmpty(TodoTask.Description))
        {
            return Result.Failure(DomainErrors.TaskErrors.InvalidDescription);
        }

        //todo: more domain errors to validate

        return Result.Success();
    }
}