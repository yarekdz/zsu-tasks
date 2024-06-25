using Mapster;
using Tasks.Application.Tasks.Create;
using Tasks.Application.Tasks.Update;
using Tasks.Domain;
using Tasks.Domain.Person;
using Tasks.Domain.States;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Domain.ValueObjects;
using Tasks.Presentation.Tasks.Requests;

namespace Tasks.Api.Mapper
{
    public class RegisterTaskMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<string, TaskCategory>()
                .MapWith(s => TaskCategory.Create(s));
            config.NewConfig<TaskCategory, string>()
                .MapWith(t => t.ToString());

            config.NewConfig<int, Priority>()
                .ConstructUsing(value => Priority.Create(value));
            config.NewConfig<Priority, int>()
                .ConstructUsing(p => p.Value);

            config.NewConfig<Guid, PersonId>()
                .MapWith(g => new PersonId(g));
            config.NewConfig<PersonId, Guid>()
                .MapWith(p => p.Value);

            config.NewConfig<TaskId, Guid>()
                .MapWith(t => t.Value);
            config.NewConfig<Guid, TaskId>()
                .ConstructUsing(p => new TaskId(p));

            config.NewConfig<UpdateTaskRequest, UpdateTaskCommand>()
                .ConstructUsing(src => new UpdateTaskCommand(
                    src.Title,
                    src.Description,
                    Priority.Create(src.Priority),
                    new PersonId(src.AssigneeId)
                ));
        }
    }
}